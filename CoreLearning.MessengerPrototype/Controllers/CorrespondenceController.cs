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
    [Route("{controller}/{action}")]
    public class CorrespondenceController : ControllerBase
    {
        public CorrespondenceController(ICorrespondenceControllerHelper helper)
        {
            this.helper = helper;
        }

        private readonly ICorrespondenceControllerHelper helper;

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendMessageAsync([FromBody] ReceiverModel userMessage)
        {
            var senderUserId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            var correspondenceId = await helper.FindChatAsync(senderUserId, userMessage.ReceiverUserId) ?? await helper.CreateChatAsync(senderUserId, userMessage.ReceiverUserId);
            if (Guid.Parse(correspondenceId).Equals(Guid.Empty))
                correspondenceId = await helper.FindChatAsync(senderUserId, userMessage.ReceiverUserId);

            await helper.SendMessageAsync(correspondenceId, senderUserId, userMessage.Description);
            await helper.SaveAsync();

            return Ok(new {status = "message sent"});
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowChatAsync(Guid receiverUserId)
        {
            var senderUserId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            var correspondenceId = await helper.FindChatAsync(senderUserId, receiverUserId);

            return Ok(new {Chat = await helper.ShowChatAsync(correspondenceId)});
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowAllChatAsync()
        {
            var senderUserId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            var chats = await helper.ShowAllChatsAsync(Guid.Parse(senderUserId));

            return Ok(new {Chats = chats});
        }
    }
}