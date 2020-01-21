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
        public async Task<IActionResult> JoinGroup(string tag,
            [FromServices] IUserRepository userRepository, [FromServices] IGroupRepository groupRepository)
        {
            var userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "userId").Value);

            var group = await groupRepository.GetByInviteLink(tag);

            var result = await userRepository.JoinGroup(userId, group.Id);

            return result ? (IActionResult) Ok() : BadRequest();
        }

        [HttpDelete("Leave/{groupId}")]
        public async Task<IActionResult> JoinGroup(int groupId,
            [FromServices] IUserRepository userRepository)
        {
            var userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "userId").Value);

            var result = await userRepository.LeaveGroup(userId, groupId);

            return result ? (IActionResult) Ok() : BadRequest();
        }
        
        public class GroupModel
        {
            public string Name { get; set; }
            public string? Description { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] GroupModel group,
            [FromServices] IGroupRepository groupRepository)
        {
            var userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "userId").Value);

            var response = await groupRepository.CreateGroup(userId, group.Name, group.Description);

            return response == -1 ? (IActionResult) BadRequest() : Ok(response);
        }

        [HttpPost("{groupId}/Open")]
        public async Task<IActionResult> MakeGroupPublic(int groupId, [FromServices] IGroupRepository groupRepository)
        {
            var result = await groupRepository.MakeGroupPublic(groupId);

            return result ? (IActionResult) Ok() : BadRequest();
        }
    }
}