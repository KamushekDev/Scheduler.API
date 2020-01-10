using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parser;

namespace API.Controllers
{
    //todo: авторизация с РОЛЯМИ БЛЯДЬ
    [Route("[Controller]")]
    public class ScheduleController : ControllerBase
    {

        [HttpPost("leti")]
        public async Task<IActionResult> ParseLetiSchedule(IFormFile excelFile, [FromServices]LetiTimetableParser parser)
        {
            if (excelFile == null || excelFile.Length == 0)
                return BadRequest();

            var stream = excelFile.OpenReadStream();

            // var parsedResult = parser.ParseTimetable(stream);
            
            //todo: Парсинг расписания
            
            return Ok();
        }
        
    }
}