using FernandoDAC.Application.Core;
using FernandoDAC.Application.ViewModels;
using FernandoDAC.Domain.Entities;
using FernandoDAC.Domain.Repositories;
using FernandoDAC.Domain.Services;
using FluentValidation;
using MediatR;

namespace FernandoDAC.Application.Users.Commands
{
    public class CreateUserCommand
    {
        public class Command : IRequest<Result<LoginUserViewModel>>
        {
            public User User { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.User).SetValidator(new UserValidator());
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
                //check if we already have user with this username
                var patients = await _userRepository.GetAllUsers();
                if (patients.Any(x => x.UserName == request.User.UserName))
                {
                    return Result<LoginUserViewModel>.Failure("User already exists with this username");
                }

                var passwordHash = _authService.ComputeSha256Hash(request.User.Password);

                request.User.Password = passwordHash;

                await _userRepository.CreateUser(request.User);

                var token = _authService.GenerateJwtToken(request.User.UserName, request.User.Role);

                return Result<LoginUserViewModel>.Success(new LoginUserViewModel() { Token = token, UserName = request.User.UserName });
            }
        }
    }
}