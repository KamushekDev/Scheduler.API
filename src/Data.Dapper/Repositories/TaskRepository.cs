using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Models;
using Contracts.Repositories;
using Data.Dapper.Models;

namespace Data.Dapper.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DatabaseAccess _databaseAccess;

        public TaskRepository(DatabaseAccess databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }

        public async Task<IEnumerable<ITask>> GetUserTasks(int userId)
        {
            const string query = 
                @"select tasks.id as taskId,
                         date_begin as dateBegin,
                         date_end as dateEnd,
                         name as taskName,
                         lesson_name as lessonName,
                         description as description
                  from tasks
                  join classes c on tasks.end_class_id = c.id
                  where c.group_id in (select group_id from users_to_groups where user_id = @userId);";
            var result = await _databaseAccess.ExecuteQueryAsync<TaskDto>(query, new {userId = userId});
            return result.Select(x => x.ToModel()).ToArray();
        }

        public async Task<int> CreateTask(int endClassId, DateTime? dateEnd, string name, string description)
        {
            const string query =
                @"insert into tasks (end_class_id, date_begin, date_end, name, description) values (@classId, NOW(), @endDate, @name, @description) returning id;";
            
            var result = await _databaseAccess.ExecuteQueryFirstOrDefaultAsync<int>(query, new {classId = endClassId, name = name, description = description, enddate = dateEnd});

            return result;
        }
    }
}