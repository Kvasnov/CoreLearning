using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.Infrastructure.Business.Mediators.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoreLearning.MessengerPrototype.Controllers
{
    [ApiController]
    [Route("Account")]
    public class AccountController : ControllerBase
    {
        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        private readonly IMediator mediator;

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
                BadRequest();

            var command = new RegisterCommand(registerModel);
            await mediator.Send(command);

            return Ok(new {Message = "New user", User = registerModel.Email});
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                BadRequest();

            var command = new LoginCommand(loginModel);
            var result = await mediator.Send(command);

            return Ok(new {access_token = result, username = loginModel.Email});
        }
    }
}