using System.Threading.Tasks;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TimetableController: Controller
    {
        private readonly ITimetableService _timetableService;

        public TimetableController(ITimetableService timetableService)
        {
            _timetableService = timetableService;
        }

        [HttpGet("/getTimetable")]
        public async Task<IActionResult> GetTimetable()
        {
            // тут типо получаем из учетки
            var groupId = 1;
            if (false)
            {
                return Unauthorized();
            }
            
            var result = await _timetableService.GetClassesByGroup(groupId);
            return Json(result);
        }
    }
}