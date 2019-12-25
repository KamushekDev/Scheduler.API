using System.Collections.Generic;
using System.Threading.Tasks;
using API.Interfaces;
using Contracts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
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
            int groupId;
            if (false)
            {
                return Unauthorized();
            }
            else
            {
                groupId = 6374;
            }
            var result = await _timetableService.GetTimetableByGroupId(groupId);
            return Json(result);
        }
    }
}