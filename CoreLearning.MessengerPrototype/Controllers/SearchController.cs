using System.Linq;
using System.Threading.Tasks;
using CoreLearning.DBLibrary.Interfaces.ControllerHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreLearning.MessengerPrototype.Controllers
{
    [ApiController]
    [Route("Search")]
    public class SearchController : ControllerBase
    {
        public SearchController(ISearchControllerHelper helper)
        {
            this.helper = helper;
        }

        private readonly ISearchControllerHelper helper;

        [HttpGet("SearchUsers")]
        [Authorize]
        public async Task<IActionResult> SearchUsersAsync(string nickname)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            var users = await helper.SearchUsersAsync(nickname, userId);

            return Ok(new {Users = users});
        }
    }
}