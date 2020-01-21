using System.Linq;
using System.Threading.Tasks;
using Contracts.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[Controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTasks([FromServices] ITaskRepository taskRepository)
        {
            var userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "userId").Value);
            var result = await taskRepository.GetUserTasks(userId);
            return Ok(result);
        }
    }
}