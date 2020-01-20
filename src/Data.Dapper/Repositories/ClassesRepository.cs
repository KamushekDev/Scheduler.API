using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Models;
using Contracts.Repositories;
using Data.Dapper.Models;

namespace Data.Dapper.Repositories
{
    public class ClassesRepository : IClassesRepository
    {
        private readonly BaseDataAcсess _bda;

        public ClassesRepository(BaseDataAcсess bda)
        {
            _bda = bda;
        }
        
        public async Task<IEnumerable<IClass>> GetUserClasses(int userId)
        {
            const string query = 
                @"select cl.id              as classId,
                         cl.lesson_name     as lessonName,
                         cl.room_name       as roomName,
                         cl.time            as classTime,
                         cl.duration        as classDuration,
                         cl.class_type_name as classType,
                         cl.week_type       as weekType,
                         cl.day_number      as dayNumber,
                         g.name             as groupName,
                         u.name             as teacherName,
                         u.surname          as teacherSurname,
                         u.patronymic       as teacherPatronymic
                  from classes cl
                           left join groups g on cl.group_id = g.id
                           left join users u on cl.teacher_id = u.id
                  where group_id in (select group_id from users_to_groups where user_id = @userId)
                    and term_id in (select id from terms where start_date < now() and now() < end_date);";
            var response = await _bda.ExecuteQuery<ClassDto>(query, new {userId = userId});

            return response.Select(x => x.ToModel()).ToArray();
        }
    }
}