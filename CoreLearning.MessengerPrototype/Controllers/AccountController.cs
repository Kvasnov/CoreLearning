using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.DBLibrary.Interfaces;
using CoreLearning.MessengerPrototype.ControllersHelpers;
using Microsoft.AspNetCore.Mvc;

namespace CoreLearning.MessengerPrototype.Controllers
{
    [ApiController]
    [Route("{controller}/{action}")]
    public class AccountController : ControllerBase
    {
        public AccountController(ITokenService tokenService, AccountControllerHelper helper)
        {
            this.helper = helper;
            this.tokenService = tokenService;
        }

        private readonly AccountControllerHelper helper;

        private readonly ITokenService tokenService;

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

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                BadRequest();

            if (!helper.CheckUserIsCreated(loginModel.Email, loginModel.Password))
                return BadRequest();

            var encodedJwt = tokenService.CreateToken(loginModel.Email, helper.GetUserId(loginModel.Email));
            var response = new {access_token = encodedJwt, username = loginModel.Email};

            return Ok(response);
        }
    }
}