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
        [HttpPost("leti")]
        public async Task<IActionResult> ParseLetiSchedule(IFormFile excelFile,
            [FromServices] LetiTimetableParser parser)
        {
            if (excelFile == null || excelFile.Length == 0)
                return BadRequest();

            var stream = excelFile.OpenReadStream();

            // var parsedResult = parser.ParseTimetable(stream);

            //todo: Парсинг расписания

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetSchedule([FromServices]IClassesRepository classesRepository)
        {
            var userId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "userId").Value);

            var response = await classesRepository.GetUserClasses(userId);
            
            return Ok(response);
        }
    }
}