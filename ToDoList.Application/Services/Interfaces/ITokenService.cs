using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Application.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateAccessToken(ApplicationUser user, string refreshTokenId);
        RefreshToken CreateRefreshToken(ApplicationUser user);
        Task<TokenResult> CreateTokenResult(ApplicationUser user);
        ExpiredTokenClaim GetExpiredTokenClaim(string token);
        Task<Result<ApplicationUser>> ValidateExpiredTokenAndReturnUserIfValid(string token, string accessToken);
    }
}
