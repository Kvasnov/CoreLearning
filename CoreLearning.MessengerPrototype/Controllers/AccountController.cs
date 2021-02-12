using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.DBLibrary.Interfaces;
using CoreLearning.MessengerPrototype.ControllersHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreLearning.MessengerPrototype.Controllers
{
    [ApiController]
    [Route("{controller}/{action}")]
    public class AccountController : ControllerBase
    {
        public AccountController(ITokenService authorization, AccountControllerHelper helper)
        {
            this.helper = helper;
            this.authorization = authorization;
        }

        private readonly ITokenService authorization;

        private readonly AccountControllerHelper helper;

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
                BadRequest();

            if (helper.CheckUserIsCreated(registerModel.Email, registerModel.Password))
                return BadRequest();

            await helper.AddUserAsync(registerModel);
            await helper.SaveAsync();

            return Ok(new {Message = "New user", User = registerModel.Email});
        }

        [HttpGet]
        [Authorize]
        public IActionResult TestMethod()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult LoginAsync(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                BadRequest();

            if (!helper.CheckUserIsCreated(loginModel.Email, loginModel.Password))
                return BadRequest();

            var encodedJwt = authorization.CreateToken(loginModel.Email, helper.GetUserId(loginModel.Email));
            var response = new {access_token = encodedJwt, username = loginModel.Email};

            return Ok(response);
        }
    }
}