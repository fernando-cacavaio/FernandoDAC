using FernandoDAC.Application.Core;
using FernandoDAC.Application.ViewModels;
using FernandoDAC.Domain.Repositories;
using FernandoDAC.Domain.Services;
using FluentValidation;
using MediatR;

namespace FernandoDAC.Application.Users.Commands
{
    public class LoginUserCommand
    {
        public class Command : IRequest<Result<LoginUserViewModel>>
        {
            public LoginViewModel User { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.User).SetValidator(new LoginUserValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<LoginUserViewModel>>
        {
            private readonly IUserRepository _userRepository;
            private readonly IAuthService _authService;

            public Handler(IUserRepository userRepository, IAuthService authService)
            {
                _userRepository = userRepository;
                _authService = authService;
            }

            public async Task<Result<LoginUserViewModel>> Handle(Command request, CancellationToken cancellationToken)
            {
                var passwordHash = _authService.ComputeSha256Hash(request.User.Password);

                var user = await _userRepository.GetUserByUserNameAndPasswordAsync(request.User.UserName, passwordHash);
                if (user == null)
                {
                    return Result<LoginUserViewModel>.Failure("Invalid credentials");
                }

                var token = _authService.GenerateJwtToken(user.UserName, user.Role);

                return Result<LoginUserViewModel>.Success(new LoginUserViewModel() { Token = token, UserName = user.UserName });
            }
        }
    }
}