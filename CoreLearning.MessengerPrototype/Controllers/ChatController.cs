using System;
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
    public class ChatController : ControllerBase
    {
        public ChatController(ChatControllerHelper helper)
        {
            this.helper = helper;
        }

        private readonly ChatControllerHelper helper;

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SendMessageAsync(ReceiverModel userMessage)
        {
            var senderUserId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            var chatId = await helper.FindChatAsync(senderUserId, userMessage.ReceiverUserId) ?? await helper.CreateChatAsync(senderUserId, userMessage.ReceiverUserId);

            if (Guid.Parse(chatId).Equals(Guid.Empty))
            {
                chatId = await helper.FindChatAsync(senderUserId, userMessage.ReceiverUserId);
            }
            await helper.SendMessageAsync(chatId, senderUserId, userMessage.Description);
            await helper.SaveAsync();

            return Ok(new {status = "message sent"});
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowChatAsync(string receiverUserId)
        {
            var senderUserId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            var chatId = await helper.FindChatAsync(senderUserId, receiverUserId);

            return Ok(new {Chat = await helper.ShowChatAsync(chatId)});
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ShowAllChatAsync()
        {
            var senderUserId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            var chats = await helper.ShowAllChatsAsync(senderUserId);

            return Ok(new {Chats = chats});
        }
    }
}