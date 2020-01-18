using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Models;
using Dapper;
using Data.Dapper.Interfaces;

namespace Data.Dapper.Repositories
{
    public class TimetableRepository : ITimetableRepository
    {
        private readonly BaseDataAcсess _baseDataAcсess;
        private const string TestQuery = "select * from classes";

        private const string Query = @"select
											t.end_class_id as TaskClassId,
											t.date_begin as TaskStartTime,
											t.date_end as TaskEndTime,
											t.name as TaskName,
											t.description as TaskDescription,
											t.access as TaskAccess,
											cl.lesson_name as LessonName,
											cl.room_name as RoomName,
											cl.group_id as GroupId,
											cl.time as ClassTime,
											cl.access as ClassAccess,
											cl.class_type_name as ClassTypeName,
											cl.term_dates_id as ClassTermDatesId,
											cl.teacher_id as ClassTeacherId,
											ct.description as ClassTypeDescription,
											r.description as RoomDescrprion,
											td.start_date as TermStartDate,
											td.end_date as TermEndDate,
											l.description as LessonDescription,
											g.name as GroupName,
											g.access as GroupAccess,
											g.invite_tag as GroupInviteTag,
											g.description as GroupDescription,
											u.name as TeacherName,
											u.surname as TeacherSurname,
											u.patronymic as TeacherPatronymic,
											u.phone as TeacherPhone,
											u.email as TeacherEmail
											from tasks t
												left join classes cl on t.end_class_id = cl.id
												left join class_types ct on cl.class_type_name = ct.name
												left join rooms r on cl.room_name = r.name
												left join term_dates td on cl.term_dates_id = td.id
												left join lessons l on cl.lesson_name = l.name
												left join groups g on cl.group_id = g.id
												left join users u on cl.teacher_id = u.id;";

        public TimetableRepository(BaseDataAcсess baseDataAcсess)
        {
            _baseDataAcсess = baseDataAcсess;
        }

        public async Task<IEnumerable<Class>> GetTimetableByGroups(IEnumerable<string> groups)
        {
            var dbResult = await _baseDataAcсess.ExecuteQueryWhichReturnsCollectionAsync<DbModel>(Query);
            var result = new List<Class>();
            foreach (var item in dbResult)
            {
                Console.WriteLine("ti pidaras");
            }

            return result;
        }
    }
}