using FernandoDAC.Application.ViewModels;
using FluentValidation;

namespace FernandoDAC.Application.Users
{
    public class LoginUserValidator : AbstractValidator<LoginViewModel>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}