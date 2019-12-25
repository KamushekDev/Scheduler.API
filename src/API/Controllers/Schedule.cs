using System.Threading.Tasks;
using Contracts.Models;
using Dapper;
using Data.Dapper.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class Schedule
    {
        private BaseDataAcсess _dataAccess;
        
        public Schedule(BaseDataAcсess dataAcсess)
        {
            _dataAccess = dataAcсess;
        }
        
        [HttpGet("{group}")]
        public async Task<IActionResult> GetSchedule(string group)
        {
            var lessons = await _dataAccess.ExecuteStoredProcedureWhichReturnsCollectionAsync<Lesson>($"select\n       class_time_begin as start_time,\n       class_time_end as end_time,\n       day_number,\n       l.description as lesson_description,\n       lesson_names.name as lesson_name,\n       rooms.name as room,\n       u.name as teacher_name,\n       u.surname as teacher_surname,\n       u.patronymic as teacher_patronymic,\n       ct.name as lesson_type\nfrom classes cl\njoin lessons l on cl.id_lesson = l.id\njoin lesson_names on l.id_primary_name = lesson_names.id\njoin classrooms rooms on cl.id_classroom = rooms.id\njoin groups g on cl.id_group = g.id\njoin users u on cl.id_teacher = u.id\njoin class_types ct on cl.id_class_type = ct.id\nwhere g.name={group}\n;");
            return new OkObjectResult(lessons);
        } 
        
    }
}