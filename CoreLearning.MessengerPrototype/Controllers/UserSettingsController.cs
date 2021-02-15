using System.Linq;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.MessengerPrototype.ControllersHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreLearning.MessengerPrototype.Controllers
{
    [ApiController]
    [Route("{controller}/{action}")]
    public class UserSettingsController : ControllerBase
    {
        public UserSettingsController(UserSettingsControllerHelper helper)
        {
            this.helper = helper;
        }

        private readonly UserSettingsControllerHelper helper;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowUserSettingsAsync()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

            return Ok(new {Settings = await helper.GetUserSettingsAsync(userId)});
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangeUserSettingsAsync(ChangeUserSettingsModel newSettings)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            var settings = await helper.SetUserSettingsAsync(userId, newSettings);
            await helper.SaveAsync();

            return Ok(new {Message = "Новые настройки применены", Settings = settings});
        }
    }
}