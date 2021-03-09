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
    [Route("Correspondence")]
    public class CorrespondenceController : ControllerBase
    {
        public CorrespondenceController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        private readonly IMediator mediator;

        [HttpPost("SendMessage")]
        [Authorize]
        public async Task<IActionResult> SendMessageAsync([FromBody] ReceiverModel userMessage)
        {
            //проверка могут ли пользователю писать не друзья??
            //проверка, не заблокирован ли пользователь
            var senderUserId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

            if (string.IsNullOrEmpty(senderUserId))
                return BadRequest();

            var command = new SendMessageCommand(userMessage, Guid.Parse(senderUserId));
            await mediator.Send(command);

            return Ok(new {status = "message sent"});
        }

        [HttpGet("ShowChat")]
        [Authorize]
        public async Task<IActionResult> ShowChatAsync(Guid receiverUserId)
        {
            var senderUserId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

            if (string.IsNullOrEmpty(senderUserId))
                return BadRequest();

            var query = new ShowChatQuery(Guid.Parse(senderUserId), receiverUserId);
            var result = await mediator.Send(query);

            return Ok(new {Chat = result});
        }

        [HttpGet("ShowAllChat")]
        [Authorize]
        public async Task<IActionResult> ShowAllChatAsync()
        {
            var senderUserId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

            if (string.IsNullOrEmpty(senderUserId))
                return BadRequest();

            var query = new ShowAllChatQuery(Guid.Parse(senderUserId));
            var result = await mediator.Send(query);

            return Ok(new {Chats = result});
        }
    }
}