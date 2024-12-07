using MediatR;
using ToDoList.Application.Dtos.Auth;
using ToDoList.Application.Helpers;

namespace ToDoList.Application.Features.Auth.Requests.Commands
{
    public class RefreshTokenCommand : IRequest<Result<object>>
    {
        public RefreshTokenDto RefreshTokenDto { get; set; }
    }
}
