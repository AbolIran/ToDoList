using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ToDoList.Application.Features.Auth.Requests.Commands;
using ToDoList.Application.Helpers;
using ToDoList.Application.Services.Interfaces;

namespace RazorApp.Application.Features.Auth.Handlers.Commands
{
    internal class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<object>>
    {
        private readonly ILogger<RefreshTokenCommandHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenService _tokenService;
        public async Task<Result<object>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var authToken = _httpContextAccessor.HttpContext.GetAuthToken();
                var result = await _tokenService.ValidateExpiredTokenAndReturnUserIfValid(request.RefreshTokenDto.RefreshToken, authToken);

                if (result.ResultCode == 200)
                {
                    var tokenResult = await _tokenService.CreateTokenResult(result.Value);

                    return Result<object>.Success(200, "operation succeeded", new
                    {
                        AccessToken = tokenResult.AccessToken,
                        RefreshToken = tokenResult.RefreshToken,
                    });
                }

                return Result<object>.Failure(400, result.ResultMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Result<object>.Failure(500, "there was a problem");
            }
        }
    }
}
