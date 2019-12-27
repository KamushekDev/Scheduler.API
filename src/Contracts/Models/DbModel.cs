using System;
using Newtonsoft.Json;

namespace Contracts.Models
{
    [JsonObject]
    public class DbModel
    {
        [JsonProperty("start_time")]
        public TimeSpan StartTime { get; set; }
        
        [JsonProperty("end_time")]
        public TimeSpan EndTime { get; set; }
        
        [JsonProperty("class_day_number")]
        public int DayNumber { get; set; }
        
        [JsonProperty("lesson_name")]
        public string LessonName { get; set; }
        
        [JsonProperty("room")]
        public string Room { get; set; }
        
        [JsonProperty("teacher_name")]
        public string TeacherName { get; set; }
        
        [JsonProperty("teacher_surname")]
        public string TeacherSurname { get; set; }
        
        [JsonProperty("teacher_patronymic")]
        public string TeacherPatronymic { get; set; }
        
        [JsonProperty("class_type")]
        public string ClassType { get; set; }
        
        [JsonProperty("group_name")]
        public string GroupName { get; set; }
    }
}