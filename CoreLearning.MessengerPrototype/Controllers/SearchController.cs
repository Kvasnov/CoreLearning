using System.Linq;
using System.Threading.Tasks;
using CoreLearning.MessengerPrototype.ControllersHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreLearning.MessengerPrototype.Controllers
{
    [ApiController]
    [Route("{controller}/{action}")]
    public class SearchController : ControllerBase
    {
        public SearchController(SearchControllerHelper helper)
        {
            this.helper = helper;
        }

        private readonly SearchControllerHelper helper;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SearchUsersAsync(string nickname)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;
            var users = await helper.SearchUsersAsync(nickname, userId);

            return Ok(new {Users = users});
        }
    }
}