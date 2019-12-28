using System;
using Newtonsoft.Json;

namespace Contracts.Models
{
    [JsonObject]
    public class DbModel
    {
        public TimeSpan StartTime { get; set; }
        
        public TimeSpan EndTime { get; set; }
        
        public int ClassDayNumber { get; set; }
        
        public string LessonName { get; set; }
        
        public string Room { get; set; }
        
        public string TeacherName { get; set; }
        
        public string TeacherSurname { get; set; }
        
        public string TeacherPatronymic { get; set; }
        
        public string ClassType { get; set; }
        
        public string GroupName { get; set; }
    }
}