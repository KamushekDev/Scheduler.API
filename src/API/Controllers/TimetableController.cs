using System.Collections.Generic;
using System.Threading.Tasks;
using API.Interfaces;
using Contracts.Models;
using Microsoft.AspNetCore.Authorization;
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
            var groups = new List<string>();
            if (false)
            {
                return Unauthorized();
            }
            else
            {
                groups.Add("6374");
                groups.Add("6371");
            }
            var result = await _timetableService.GetTimetableByGroups(groups);
            return Json(result);
        }
    }
}