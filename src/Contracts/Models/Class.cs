using System;
using Contracts.Enums;
using Newtonsoft.Json;

namespace Contracts.Models
{
    [JsonObject]
    public class Class
    {
        [JsonProperty]
        public TimeSpan StartTime { get; set; }
        [JsonProperty]
        public TimeSpan EndTime { get; set; }
        [JsonProperty]
        public int DayNumber { get; set; }
        [JsonProperty]
        public ClassTypes Types { get; set; }
        [JsonProperty]
        public Teacher Teacher { get; set; }
        [JsonProperty]
        public Classroom Classroom { get; set; }
        [JsonProperty]
        public Lesson Lesson { get; set; }
    }
}