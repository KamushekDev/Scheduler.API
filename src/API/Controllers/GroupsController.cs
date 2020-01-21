using System.Linq;
using System.Threading.Tasks;
using Contracts.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[Controller]")]
    public class GroupsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUserGroups([FromServices] IGroupRepository groupRepository)
        {
            var userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "userId").Value);

            var response = await groupRepository.GetUserGroups(userId);

            return Ok(response);
        }

        [HttpGet("Join")]
        public async Task<IActionResult> GetPublicGroupsWithoutMe([FromServices] IGroupRepository groupRepository)
        {
            var userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "userId").Value);

            var response = await groupRepository.GetPublicGroupsWithoutUser(userId);

            return Ok(response);
        }

        [HttpPost("Join/{tag}")]
        public async Task<IActionResult> GetPublicGroupsWithoutMe(string tag,
            [FromServices] IUserRepository userRepository, [FromServices] IGroupRepository groupRepository)
        {
            var userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "userId").Value);

            var group = await groupRepository.GetByInviteLink(tag);

            var result = await userRepository.JoinGroup(userId, group.Id);

            return result ? (IActionResult) Ok() : BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromServices] IGroupRepository groupRepository)
        {
            var userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "userId").Value);

            var response = await groupRepository.GetPublicGroupsWithoutUser(userId);

            return Ok(response);
        }

        [HttpPost("{groupId}/Open")]
        public async Task<IActionResult> MakeGroupPublic(int groupId, [FromServices] IGroupRepository groupRepository)
        {
            var result = await groupRepository.MakeGroupPublic(groupId);

            return result ? (IActionResult) Ok() : BadRequest();
        }
    }
}