using System;
using Contracts.Models;

namespace Data.Dapper.Models
{
    public class TaskDto
    {
        public int taskid { get; set; }
        public DateTime datebegin { get; set; }
        public DateTime dateend { get; set; }
        public string taskname { get; set; }
        public string lessonname { get; set; }
        public string description { get; set; }

        public Task ToModel() => new Task(taskid, datebegin, dateend, taskname, lessonname, description);

        public class Task : ITask
        {
            public int Id { get; }
            public DateTime Begin { get; }
            public DateTime End { get; }
            public string TaskName { get; }
            public string LessonName { get; }
            public string Description { get; }

            public Task(int id, DateTime begin, DateTime end, string taskName, string lessonName, string description)
            {
                Id = id;
                Begin = begin;
                End = end;
                TaskName = taskName;
                LessonName = lessonName;
                Description = description;
            }
        }
    }
}