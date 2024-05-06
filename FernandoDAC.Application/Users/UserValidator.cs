using FernandoDAC.Domain.Entities;
using FluentValidation;

namespace FernandoDAC.Application.Users
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Role).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}