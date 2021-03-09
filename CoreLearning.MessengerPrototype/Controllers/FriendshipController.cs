using System;
using System.Linq;
using System.Threading.Tasks;
using CoreLearning.Infrastructure.Business.Mediators.Commands;
using CoreLearning.Infrastructure.Business.Mediators.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreLearning.MessengerPrototype.Controllers
{
    [ApiController]
    [Route("Friendship")]
    public class FriendshipController : ControllerBase
    {
        public FriendshipController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        private readonly IMediator mediator;

        [HttpGet("AddToFriends")]
        [Authorize]
        public async Task<IActionResult> AddToFriendsAsync(Guid friendId)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var query = new AddToFriendsQuery(Guid.Parse(userId), friendId);
            await mediator.Send(query);

            return Ok(new {Message = $"application of friendship is send, id = {friendId}"});
        }

        [HttpPost("ShowFriedList")]
        [Authorize]
        public async Task<IActionResult> ShowFriedListAsync()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var command = new ShowFriedListCommand(Guid.Parse(userId));
            var result = await mediator.Send(command);

            return Ok(new {FriendList = result});
        }

        [HttpPost("ShowInboxApplicationList")]
        [Authorize]
        public async Task<IActionResult> ShowInboxApplicationListAsync()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var command = new ShowInboxApplicationListCommand(Guid.Parse(userId));
            var result = await mediator.Send(command);

            return Ok(new {InboxApplicationList = result});
        }

        [HttpPost("ShowOutboxApplicationList")]
        [Authorize]
        public async Task<IActionResult> ShowOutboxApplicationListAsync()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var command = new ShowOutboxApplicationCommand(Guid.Parse(userId));
            var result = await mediator.Send(command);

            return Ok(new {OutboxApplicationList = result});
        }

        [HttpGet("ApproveApplication")]
        [Authorize]
        public async Task<IActionResult> ApproveApplicationAsync(Guid friendId)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var query = new ApproveApplicationQuery(Guid.Parse(userId), friendId);
            await mediator.Send(query);

            return Ok(new {Message = $"Application is approved, friend id = {friendId}"});
        }

        [HttpGet("RemoveFriend")]
        [Authorize]
        public async Task<IActionResult> RemoveFriendAsync(Guid friendId)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var query = new RemoveFriendQuery(Guid.Parse(userId), friendId);
            await mediator.Send(query);

            return Ok(new {Message = $"Friend is removed, id = {friendId}"});
        }
    }
}