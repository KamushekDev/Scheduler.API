using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("[Controller]")]
    public class ScheduleController : ControllerBase
    {

        [HttpPost("leti")]
        public async Task<IActionResult> ParseLetiSchedule(IFormFile excelFile)
        {
            

            return Ok();
        }
        
    }
}