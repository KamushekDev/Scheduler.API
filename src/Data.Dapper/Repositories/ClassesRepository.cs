using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Models;
using Contracts.Parser;
using Contracts.Repositories;
using Data.Dapper.Models;
using Npgsql;

namespace Data.Dapper.Repositories
{
    public class ClassesRepository : IClassesRepository
    {
        private readonly DatabaseAccess _databaseAccess;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;

        public ClassesRepository(DatabaseAccess databaseAccess, IGroupRepository groupRepository,
            IUserRepository userRepository)
        {
            _databaseAccess = databaseAccess;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
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
            var response = await _databaseAccess.ExecuteQueryAsync<ClassDto>(query, new {userId = userId});

            return response.Select(x => x.ToModel()).ToArray();
        }

        public async Task<int> AddClasses(int userId, string groupName, IEnumerable<ILesson> lessons)
        {
            lessons = lessons.ToArray();

            Debug.Assert(lessons.All(x => x.Group == groupName));

            var groupId = await _groupRepository.CreateGroup(userId, groupName);

            var lessonNames = lessons.Select(x => x.Subject.Name).Distinct();
            await Task.WhenAll(lessonNames.Select(CreateLesson)); //adding lessons

            var roomsNames = lessons.Select(x => x.Room).Distinct();
            await Task.WhenAll(roomsNames.Select(CreateRoom)); //adding rooms

            var types = lessons.Select(x => x.Type.ToString()).Distinct();
            await Task.WhenAll(types.Select(CreateType)); //adding types

            var users = lessons.Select(x => x.Teacher).Distinct(new TeacherComparer());

            var teachers = new Dictionary<ITeacher, int>(new TeacherComparer());

            foreach (var user in users)
            {
                var teacherId = await _userRepository.AddUser(user.Surname, user.Name, user.Patronymic);
                teachers.Add(user, teacherId);
            } //adding teacher

            int count = 0;

            foreach (var lesson in lessons)
            {
                try
                {
                    var result = await CreateClass(lesson.Subject.Name, lesson.Room, groupId, lesson.Time.TimeOfDay,
                        lesson.Type.ToString(), teachers[lesson.Teacher], lesson.WeekType,
                        ((int) lesson.Day) == 0 ? 7 : ((int) lesson.Day));

                    if (result > 0)
                        count++;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"----------ERROR: {e}");
                }
            }

            return count;
        }

        private async Task<bool> CreateType(string typeName)
        {
            const string getQuery =
                @"select count(*) from class_types where name=@name";

            var result = await _databaseAccess.ExecuteQueryFirstOrDefaultAsync<int>(getQuery, new {name = typeName});

            if (result == 1)
                return true;

            const string insertQuery =
                @"insert into class_types (name) values (@name)";
            result = await _databaseAccess.ExecuteAsync(insertQuery, new {name = typeName});
            return result == 1;
        }

        private async Task CreateRoom(string roomName)
        {
            const string getQuery =
                @"select count(*) from rooms where name=@name";

            var result = await _databaseAccess.ExecuteQueryFirstOrDefaultAsync<int>(getQuery, new {name = roomName});

            if (result == 1)
                return;

            const string insertQuery =
                @"insert into rooms (name) values (@name)";
            result = await _databaseAccess.ExecuteAsync(insertQuery, new {name = roomName});
            if (result != 1)
                throw new InvalidDataException();
        }

        private class TeacherComparer : IEqualityComparer<ITeacher>
        {
            public bool Equals(ITeacher x, ITeacher y)
            {
                return x?.Name == y?.Name && x?.Surname == y?.Surname;
            }

            public int GetHashCode(ITeacher obj)
            {
                return $"{obj.Name}{obj.Surname}".GetHashCode();
            }
        }

        private async Task CreateLesson(string lessonName)
        {
            const string getQuery =
                @"select count(*) from lessons where name=@name";

            var result = await _databaseAccess.ExecuteQueryFirstOrDefaultAsync<int>(getQuery, new {name = lessonName});

            if (result == 1)
                return;

            const string insertQuery =
                @"insert into lessons (name) values (@name)";
            result = await _databaseAccess.ExecuteAsync(insertQuery, new {name = lessonName});
            if (result != 1)
                throw new InvalidDataException();
        }

        public async Task<int> CreateClass(string name, string roomName, int groupId, TimeSpan time, string classType,
            int teacherId, WeekType weekType, int dayNumber)
        {
            const string query =
                @"insert into classes (lesson_name, room_name, group_id, time, class_type_name, term_id, teacher_id, duration, week_type, day_number) 
                         values (@lessonName, @roomName, @groupId, @time, @classType, 1, @teacherId, @duration, @weekType, @dayNumber) returning id;";

            var result = await _databaseAccess.ExecuteAsync(query,
                new NpgsqlParameter("lessonName", name),
                new NpgsqlParameter("roomName", roomName),
                new NpgsqlParameter("groupId", groupId),
                new NpgsqlParameter("time", time),
                new NpgsqlParameter("classType", classType),
                new NpgsqlParameter("teacherId", teacherId),
                new NpgsqlParameter("duration", 95),
                new NpgsqlParameter("weekType", weekType),
                new NpgsqlParameter("dayNumber", dayNumber)
            );

            return result;
        }
    }
}