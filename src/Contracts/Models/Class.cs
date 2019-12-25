using System;
using Contracts.Enums;
using Newtonsoft.Json;

namespace Contracts.Models
{
    [JsonObject]
    public class Class
    {
        [JsonProperty]
        public DateTime Time { get; set; }
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