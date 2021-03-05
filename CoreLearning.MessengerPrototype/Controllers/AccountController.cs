using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.DBLibrary.Interfaces;
using CoreLearning.DBLibrary.Interfaces.ControllerHelpers;
using CoreLearning.Infrastructure.Business.Mediators.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoreLearning.MessengerPrototype.Controllers
{
    [ApiController]
    [Route("Account")]
    public class AccountController : ControllerBase
    {
        public AccountController(ITokenService tokenService, IAccountControllerHelper helper, IMediator mediator)
        {
            this.helper = helper;
            this.tokenService = tokenService;
            this.mediator = mediator;
        }

        private readonly IAccountControllerHelper helper;

        private readonly IMediator mediator;

        private readonly ITokenService tokenService;

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
                BadRequest();

            var command = new RegisterCommand(registerModel);
            var result = mediator.Send(command);

            return result.IsCanceled ? (IActionResult) Ok(new {Message = "New user", User = registerModel.Email}) : NotFound();

            //if (helper.CheckUserIsCreated(registerModel.Email, registerModel.Password))
            //    return BadRequest();

            //await helper.AddUserAsync(registerModel);
            //await helper.SaveAsync();

            //return Ok(new {Message = "New user", User = registerModel.Email});
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                BadRequest();

            //if (!helper.CheckUserIsCreated(loginModel.Email, loginModel.Password))
            //    return BadRequest();
            var encodedJwt = tokenService.CreateToken(loginModel.Email, helper.GetUserId(loginModel.Email));
            var response = new {access_token = encodedJwt, username = loginModel.Email};

            return Ok(response);
        }
    }
}