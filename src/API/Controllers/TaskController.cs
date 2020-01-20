using System.Threading.Tasks;
using Contracts.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[Controller]")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTasks([FromServices] ITaskRepository taskRepository)
        {
            var userId = 1;
            var result = await taskRepository.GetUserTasks(userId);
            return Ok(result);
        }
    }
}