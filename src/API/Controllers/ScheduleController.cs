using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parser;

namespace API.Controllers
{
    //[Authorize]
    [Route("[Controller]")]
    public class ScheduleController : ControllerBase
    {

        [HttpPost("leti")]
        public async Task<IActionResult> ParseLetiSchedule(IFormFile excelFile)
        {
            if (excelFile == null || excelFile.Length == 0)
                return BadRequest();

            var stream = excelFile.OpenReadStream();

            return Ok();
        }
        
    }
}