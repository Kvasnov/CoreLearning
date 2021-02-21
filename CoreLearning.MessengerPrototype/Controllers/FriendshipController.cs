using System;
using System.Linq;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.Interfaces.ControllerHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreLearning.MessengerPrototype.Controllers
{
    [ApiController]
    [Route("{controller}/{action}")]
    public class FriendshipController : ControllerBase
    {
        public FriendshipController(IFriendshipControllerHelper helper)
        {
            this.helper = helper;
        }

        private readonly IFriendshipControllerHelper helper;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddToFriendsAsync(Guid friendId)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            await helper.AddToFriendsAsync(Guid.Parse(userId), friendId);
            await helper.SaveAsync();

            return Ok(new {Message = "application of friendship is send"});
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ShowFriedListAsync()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

            return Ok(new {FriendList = await helper.ShowFriedListAsync(Guid.Parse(userId))});
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ShowInboxApplicationListAsync()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

            return Ok(new {InboxApplicationList = await helper.ShowInboxApplicationListAsync(Guid.Parse(userId))});
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ShowOutboxApplicationListAsync()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

            return Ok(new {InboxApplicationList = await helper.ShowOutboxApplicationListAsync(Guid.Parse(userId))});
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ApproveApplicationAsync(Guid friendId)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            await helper.ApproveApplicationAsync(Guid.Parse(userId), friendId);
            await helper.SaveAsync();

            return Ok(new {Message = "Application is approved"});
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> RemoveFriendAsync(Guid friendId)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            await helper.RemoveFriendAsync(Guid.Parse(userId), friendId);
            await helper.SaveAsync();

            return Ok(new {Message = "Friend is removed"});
        }
    }
}