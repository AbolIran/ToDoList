using ToDoList.Application.Helpers;
using ToDoList.Application.Services.Models;
using ToDoList.Domain;
using ToDoList.Domain.Auth;

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
