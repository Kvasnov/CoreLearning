using System;
using System.Linq;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.Infrastructure.Business.Mediators.Commands;
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
        public UserSettingsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        private readonly IMediator mediator;

        [HttpGet("ShowUserSettings")]
        [Authorize]
        public async Task<IActionResult> ShowUserSettingsAsync()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var query = new ShowUserSettingsQuery(Guid.Parse(userId));
            var result = await mediator.Send(query);

            return result != null ? (IActionResult) Ok(new {Settings = result}) : NotFound();
        }

        [HttpPost("ChangeUserSettings")]
        [Authorize]
        public async Task<IActionResult> ChangeUserSettingsAsync([FromBody] ChangeUserSettingsModel newSettings)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var command = new ChangeUserSettingsCommand(Guid.Parse(userId), newSettings);
            var result = await mediator.Send(command);

            return result != null ? (IActionResult) Ok(new {Message = "Новые настройки применены", Settings = result}) : NotFound();
        }
    }
}