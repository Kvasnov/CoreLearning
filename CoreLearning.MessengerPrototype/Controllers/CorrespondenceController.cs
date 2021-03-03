using System;
using System.Linq;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.DTO_models;
using CoreLearning.DBLibrary.Interfaces.ControllerHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreLearning.MessengerPrototype.Controllers
{
    [ApiController]
    [Route("Correspondence")]
    public class CorrespondenceController : ControllerBase
    {
        public CorrespondenceController(ICorrespondenceControllerHelper helper)
        {
            this.helper = helper;
        }

        private readonly ICorrespondenceControllerHelper helper;

        [HttpPost("SendMessage")]
        [Authorize]
        public async Task<IActionResult> SendMessageAsync([FromBody] ReceiverModel userMessage)
        {//проверка могут ли пользователю писать не друзья??
            //проверка, не заблокирован ли пользователь 
            var senderUserId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            var correspondenceId = await helper.FindChatAsync(senderUserId, userMessage.ReceiverUserId) ?? await helper.CreateChatAsync(senderUserId, userMessage.ReceiverUserId);
            if (Guid.Parse(correspondenceId).Equals(Guid.Empty))
                correspondenceId = await helper.FindChatAsync(senderUserId, userMessage.ReceiverUserId);

            await helper.SendMessageAsync(correspondenceId, senderUserId, userMessage.Description);
            await helper.SaveAsync();

            return Ok(new {status = "message sent"});
        }

        [HttpGet("ShowChat")]
        [Authorize]
        public async Task<IActionResult> ShowChatAsync(Guid receiverUserId)
        {
            var senderUserId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            var correspondenceId = await helper.FindChatAsync(senderUserId, receiverUserId);

            return Ok(new {Chat = await helper.ShowChatAsync(correspondenceId)});
        }

        [HttpGet("ShowAllChat")]
        [Authorize]
        public async Task<IActionResult> ShowAllChatAsync()
        {
            var senderUserId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            var chats = await helper.ShowAllChatsAsync(Guid.Parse(senderUserId));

            return Ok(new {Chats = chats});
        }
    }
}