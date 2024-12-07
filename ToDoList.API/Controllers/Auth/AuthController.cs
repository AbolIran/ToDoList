using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Dtos.Auth;
using ToDoList.Application.Features.Auth.Requests.Commands;

namespace ToDoList.API.Controllers.Auth
{
    [Route("api/[controller]/[action]")]
    [ControllerName("Auth")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        //[HttpPost]
        //public async Task<IActionResult> SendOtp(OtpDto otpDto)
        //{
        //    return HandleResult(await Mediator.Send(new SendOtpCommand { OtpDto = otpDto }));
        //}
        //[HttpPost]
        //public async Task<IActionResult> VerifyOtp(VerifyOtpDto verifyOtpDto)
        //{
        //    return HandleResult(await Mediator.Send(new VerifyOtpCommand { VerifyOtpDto = verifyOtpDto }));
        //}
        [HttpPost]
        public async Task<IActionResult> RefreshToken(RefreshTokenDto refreshTokenDto)
        {
            return HandleResult(await Mediator.Send(new RefreshTokenCommand { RefreshTokenDto = refreshTokenDto }));
        }
        //[HttpPost]
        //public async Task<IActionResult> Logout(LogoutDto logoutDto)
        //{
        //    return CommandResult(await Mediator.Send(new LogoutCommand { LogoutDto = logoutDto }));
        //}
    }
}
