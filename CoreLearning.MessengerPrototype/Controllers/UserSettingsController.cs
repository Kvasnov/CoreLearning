using System.Linq;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.DBLibrary.Interfaces.ControllerHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreLearning.MessengerPrototype.Controllers
{
    [ApiController]
    [Route("UserSettings")]
    public class UserSettingsController : ControllerBase
    {
        public UserSettingsController(IUserSettingsControllerHelper helper)
        {
            this.helper = helper;
        }

        private readonly IUserSettingsControllerHelper helper;

        [HttpGet("ShowUserSettings")]
        [Authorize]
        public async Task<IActionResult> ShowUserSettingsAsync()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

            return Ok(new {Settings = await helper.GetUserSettingsAsync(userId)});
        }

        [HttpPost("ChangeUserSettings")]
        [Authorize]
        public async Task<IActionResult> ChangeUserSettingsAsync([FromBody] ChangeUserSettingsModel newSettings)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            var settings = await helper.SetUserSettingsAsync(userId, newSettings);
            await helper.SaveAsync();

            return Ok(new {Message = "Новые настройки применены", Settings = settings});
        }
    }
}