using FluentValidation;

namespace ToDoList.Application.Dtos.Auth.validators
{
    public class RefreshTokenDtoValidator : AbstractValidator<RefreshTokenDto>
    {
        public RefreshTokenDtoValidator()
        {
            Include(new IRefreshTokenDtoValidator());
        }
    }
}
