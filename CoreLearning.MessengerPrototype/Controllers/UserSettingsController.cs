using System;
using System.Linq;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.DBLibrary.Interfaces.ControllerHelpers;
using CoreLearning.Infrastructure.Business.Mediators.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreLearning.MessengerPrototype.Controllers
{
    [ApiController]
    [Route("UserSettings")]
    public class UserSettingsController : ControllerBase
    {
        public UserSettingsController(IUserSettingsControllerHelper helper, IMediator mediator)
        {
            this.helper = helper;
            this.mediator = mediator;
        }

        private readonly IUserSettingsControllerHelper helper;

        private readonly IMediator mediator;

        [HttpGet("ShowUserSettings")]
        [Authorize]
        public async Task<IActionResult> ShowUserSettingsAsync()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            var query = new ShowUserSettingsQuery(Guid.Parse(userId));
            var result = await mediator.Send(query);

            return result != null ? (IActionResult) Ok(new {Settings = result}) : NotFound();
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