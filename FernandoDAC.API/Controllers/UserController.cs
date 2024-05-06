using FernandoDAC.Application.Users.Commands;
using FernandoDAC.Application.ViewModels;
using FernandoDAC.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FernandoDAC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiBaseController
    {
        public UserController()
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            return HandleResult(await Mediator.Send(new CreateUserCommand.Command { User = user }));
        }

        [HttpPut]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            return HandleResult(await Mediator.Send(new LoginUserCommand.Command { User = user }));
        }
    }
}