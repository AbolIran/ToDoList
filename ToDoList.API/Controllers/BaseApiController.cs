using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Helpers;

namespace ToDoList.API.Controllers
{
    [RequireHttps]
    [Route("[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            return result.ResultCode switch
            {
                200 => Ok(result),
                400 or -400 or -1400 => BadRequest(result),
                404 or -404 => NotFound(result),
                1200 => Ok(result),
                _ => BadRequest(result),
            };
        }

        protected ActionResult CommandResult<T>(Result<T> result)
        {
            return result.ResultCode switch
            {
                200 => Ok(result),
                400 or -400 or -1400 => BadRequest(result),
                404 or -404 => NotFound(result),
                _ => BadRequest(result),
            };
        }
    }
}