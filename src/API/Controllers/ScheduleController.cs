using System.Linq;
using System.Threading.Tasks;
using Contracts.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parser;

namespace API.Controllers
{
    //todo: авторизация с РОЛЯМИ БЛЯДЬ
    [Route("[Controller]")]
    [Authorize]
    public class ScheduleController : ControllerBase
    {
        [HttpPost("leti/{groupName}")]
        public async Task<IActionResult> ParseLetiSchedule(string groupName, [FromForm]IFormFile excelFile, 
            [FromServices] LetiTimetableParser parser, [FromServices] IClassesRepository classesRepository)
        {
            if (excelFile == null || excelFile.Length == 0)
                return BadRequest();

            var userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "userId").Value);


            var stream = excelFile.OpenReadStream();

            var parsedResult = await parser.ParseTimetable(stream);

            var lessons = parsedResult.Lessons.Where(x => x.Group == groupName).ToArray();

            var result = lessons.Any() ? await classesRepository.AddClasses(userId, groupName, lessons) : 0;

            return Ok(result.ToString());
        }

        [HttpGet]
        public async Task<IActionResult> GetSchedule([FromServices] IClassesRepository classesRepository)
        {
            var userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "userId").Value);

            var response = await classesRepository.GetUserClasses(userId);

            return Ok(response);
        }
    }
}