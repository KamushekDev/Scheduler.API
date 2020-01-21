using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Repositories;
using DocumentFormat.OpenXml.Math;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("[Controller]")]
    [ApiController]
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

        public class TaskModel
        {
            public int ClassId { get; set; }
            public DateTime? EndDate { get; set; }
            public string Name { get; set; }
            public string? Description { get; set; } = null;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskModel task,
            [FromServices] ITaskRepository taskRepository)
        {
            var result = await taskRepository.CreateTask(task.ClassId, task.EndDate, task.Name, task.Description);

            return Ok(result);
        }
    }
}