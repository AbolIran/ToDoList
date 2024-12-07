using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ToDoList.Application.Contracts.Persistence.Auth;
using ToDoList.Application.Helpers;
using ToDoList.Application.Services.Interfaces;
using ToDoList.Application.Services.Models;
using ToDoList.Domain.Auth;
using ToDoList.Domain;

namespace ToDoList.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IRefreshTokenRepository refreshTokenRepository,
            IConfiguration config, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<TokenService> logger)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _config = config;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public string CreateAccessToken(ApplicationUser user, string refreshTokenId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("rtid",refreshTokenId)
            };

            var userClaims = _userManager.GetClaimsAsync(user).Result;
            claims.AddRange(userClaims);

            var userRoles = _userManager.GetRolesAsync(user).Result;
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));

                var role = _roleManager.FindByNameAsync(userRole).Result;

                if (role != null)
                {
                    var roleClaims = _roleManager.GetClaimsAsync(role).Result;

                    foreach (var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(120),
                SigningCredentials = creds,
                Audience = _config["JwtSettings:Audience"],
                Issuer = _config["JwtSettings:Issuer"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        public RefreshToken CreateRefreshToken(ApplicationUser user)
        {
            string refreshToken;
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken = Convert.ToBase64String(randomNumber);
            }
            refreshToken += Guid.NewGuid().ToString();

            return new RefreshToken
            {
                Id = Guid.NewGuid(),
                ExpireAt = DateTime.Now.AddHours(200),
                DateCreated = DateTime.Now,
                Token = refreshToken,
                ApplicationUser = user
            };
        }
        public async Task<TokenResult> CreateTokenResult(ApplicationUser user)
        {
            var refreshTokenFullObject = CreateRefreshToken(user);
            try
            {
                await _refreshTokenRepository.Add(refreshTokenFullObject);
            }
            catch
            {
                _logger.LogCritical("Some Problem Occured in Saving the RefreshToken");
            }

            var accessToken = CreateAccessToken(user, refreshTokenFullObject.Id.ToString());

            var tokenResult = new TokenResult
            {
                RefreshToken = refreshTokenFullObject.Token,
                AccessToken = accessToken
            };

            return tokenResult;
        }
        public ExpiredTokenClaim GetExpiredTokenClaim(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var validator = new JwtSecurityTokenHandler();

            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = "AtorinaUser",
                ValidIssuer = "Atorina",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            if (token.StartsWith("Bearer "))
            {
                token = token.Substring("Bearer ".Length);
            }

            if (validator.CanReadToken(token))
            {
                ClaimsPrincipal principal;
                try
                {
                    principal = validator.ValidateToken(token, validationParameters, out var validatedToken);

                    var userIdClaim = principal.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).SingleOrDefault();
                    var expireClaim = principal.Claims.Where(c => c.Type == "exp").SingleOrDefault();
                    var createClaim = principal.Claims.Where(c => c.Type == "iat").SingleOrDefault();
                    var rtidClaim = principal.Claims.Where(c => c.Type == "rtid").SingleOrDefault();

                    if (userIdClaim.Value == null || expireClaim.Value == null
                        || rtidClaim.Value == null || createClaim == null)
                        return null;

                    DateTime epoch = new DateTime(1970, 1, 1, 1, 0, 0, 0).ToLocalTime();
                    var expireDateTime = epoch.AddSeconds(double.Parse(expireClaim.Value));

                    var createDateTime = epoch.AddSeconds(double.Parse(createClaim.Value));

                    return new ExpiredTokenClaim
                    {
                        UserId = userIdClaim.Value,
                        RefreshTokenId = rtidClaim.Value,
                        DateCreated = createDateTime,
                        ExpiryDate = expireDateTime
                    };
                }

                catch (SecurityTokenExpiredException)
                {
                    JwtSecurityToken expiredToken = null;

                    try
                    {
                        expiredToken = new JwtSecurityToken(token);
                    }
                    catch (ArgumentException)
                    {
                        return null;
                    }

                    if (token != null)
                    {
                        var userIdClaim = expiredToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
                        var expireClaim = expiredToken.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;
                        var createClaim = expiredToken.Claims.FirstOrDefault(c => c.Type == "iat")?.Value;
                        var rtidClaim = expiredToken.Claims.FirstOrDefault(c => c.Type == "rtid")?.Value;

                        if (!string.IsNullOrEmpty(userIdClaim) && !string.IsNullOrEmpty(expireClaim)
                            && !string.IsNullOrEmpty(rtidClaim) && !string.IsNullOrEmpty(createClaim))
                        {
                            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                            var expireDateTime = epoch.AddSeconds(double.Parse(expireClaim));
                            var createDateTime = epoch.AddSeconds(double.Parse(createClaim));

                            var expiredTokenDto = new ExpiredTokenClaim
                            {
                                UserId = userIdClaim,
                                RefreshTokenId = rtidClaim,
                                DateCreated = createDateTime,
                                ExpiryDate = expireDateTime
                            };

                            return expiredTokenDto;
                        }
                    }
                }

                catch
                {
                    return null;
                }
            }
            return null;
        }
        public async Task<Result<ApplicationUser>> ValidateExpiredTokenAndReturnUserIfValid(string token, string accessToken)
        {
            var refreshToken = await _refreshTokenRepository.Get(token);
            if (refreshToken == null || accessToken == null)
                return Result<ApplicationUser>.Failure(-401, "Login Agian");

            var tokenClaims = GetExpiredTokenClaim(accessToken);
            if (tokenClaims == null) return Result<ApplicationUser>.Failure(-402, "Login Agian");


            if (refreshToken.ExpireAt < DateTime.Now)
                return Result<ApplicationUser>.Failure(-404, "Login Agian");


            if (tokenClaims.RefreshTokenId != refreshToken.Id.ToString())
                return Result<ApplicationUser>.Failure(-401, "Login Agian");

            if (tokenClaims.ExpiryDate > DateTime.Now)
            {
                return Result<ApplicationUser>.Failure(-400, "Token not Expired Yet");
            }

            var user = (await _refreshTokenRepository.Get(token)).ApplicationUser;

            if (tokenClaims.UserId != user.Id)
            {
                return Result<ApplicationUser>.Failure(-403, "Login Agian");
            }

            await _refreshTokenRepository.Delete(refreshToken);

            return Result<ApplicationUser>.Success(200, "", user);
        }
    }
}
