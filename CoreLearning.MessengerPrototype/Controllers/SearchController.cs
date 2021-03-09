using System;
using System.Linq;
using System.Threading.Tasks;
using CoreLearning.Infrastructure.Business.Mediators.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreLearning.MessengerPrototype.Controllers
{
    [ApiController]
    [Route("Search")]
    public class SearchController : ControllerBase
    {
        public SearchController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        private readonly IMediator mediator;

        [HttpGet("SearchUsers")]
        [Authorize]
        public async Task<IActionResult> SearchUsersAsync(string nickname)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var query = new SearchUsersQuery(nickname, Guid.Parse(userId));
            var result = await mediator.Send(query);

            return result != null ? (IActionResult) Ok(new {Users = result}) : NotFound();
        }
    }
}