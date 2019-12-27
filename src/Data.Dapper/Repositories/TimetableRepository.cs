using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Models;
using Dapper;
using Data.Dapper.Interfaces;

namespace Data.Dapper.Repositories
{
    public class TimetableRepository : ITimetableRepository
    {
        private BaseDataAcсess _baseDataAcсess;

        public TimetableRepository(BaseDataAcсess baseDataAcсess)
        {
            _baseDataAcсess = baseDataAcсess;
        }

        public async Task<IEnumerable<Class>> GetTimetableByGroupId(int groupId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("GroupId", groupId);
            var dbResult = await _baseDataAcсess.ExecuteStoredProcedureWhichReturnsCollectionAsync<DbModel>(
                StoredProcedures.GetTimetableProcedure);
            var result = new List<Class>();
            foreach (var item in dbResult)
            {
                result.Add(new Class
                {
                    Classroom = new Classroom {Id = item.Room}, Lesson = new Lesson {Name = item.LessonName},
                    Teacher = new Teacher
                        {Name = item.TeacherName, Surname = item.TeacherSurname, Patronymic = item.TeacherPatronymic},
                    StartTime = item.StartTime, EndTime = item.EndTime, DayNumber = item.DayNumber
                });
            }

            return result;
        }
    }
}