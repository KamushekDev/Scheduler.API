using System;
using Contracts.Models;

namespace Data.Dapper.Models
{
    public class ClassDto
    {
        public int classid { get; set; }
        public string lessonname { get; set; }
        public string roomname { get; set; }
        public TimeSpan classtime { get; set; }
        public int duration { get; set; }
        public string classtype { get; set; }
        public string groupname { get; set; }
        public string teachername { get; set; }
        public string teachersurname { get; set; }
        public string teacherpatronymic { get; set; }
        public WeekType weektype { get; set; }
        public int daynumber { get; set; }

        public Class ToModel()
        {
            var teacher = new UserDto()
            {
                name = teachername,
                surname = teachersurname,
                patronymic = teacherpatronymic
            }.ToModel();
            var classModel = new Class(classid, lessonname, roomname, classtime, duration, classtype, groupname,
                teacher, weektype, daynumber);
            return classModel;
        }

        public class Class : IClass
        {
            public int Id { get; }
            public string LessonName { get; }
            public string RoomName { get; }
            public TimeSpan StartTime { get; }
            public int Duration { get; }
            public string ClassTypeName { get; }
            public string GroupName { get; }
            public IUser Teacher { get; }
            public WeekType WeekType { get; }
            public int DayNumber { get; }

            public Class(int id, string lessonName, string roomName, TimeSpan startTime, int duration,
                string classTypeName, string groupName, IUser teacher, WeekType weekType, int dayNumber)
            {
                Id = id;
                LessonName = lessonName;
                RoomName = roomName;
                StartTime = startTime;
                Duration = duration;
                ClassTypeName = classTypeName;
                GroupName = groupName;
                Teacher = teacher;
                WeekType = weekType;
                DayNumber = dayNumber;
            }
        }
    }
}