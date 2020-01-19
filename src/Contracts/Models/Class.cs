using System;
using Contracts.Enums;
using Contracts.Interfaces.Models;
using Newtonsoft.Json;

namespace Contracts.Models
{
    [JsonObject]
    public class Class : IClass
    {
        public Class(int id, TimeSpan startTime, AccessModifier access, int duration, ILesson lesson, IRoom room,
            IGroup group, IClassType classType, ITerm term, IUser teacher)
        {
            Id = id;
            StartTime = startTime;
            Access = access;
            Duration = duration;
            Lesson = lesson;
            Room = room;
            Group = group;
            ClassType = classType;
            Term = term;
            Teacher = teacher;
        }

        [JsonProperty] public int Id { get; }
        [JsonProperty] public TimeSpan StartTime { get; }
        [JsonProperty] public AccessModifier Access { get; }
        [JsonProperty] public int Duration { get; }
        [JsonProperty] public ILesson Lesson { get; }
        [JsonProperty] public IRoom Room { get; }
        [JsonProperty] public IGroup Group { get; }
        [JsonProperty] public IClassType ClassType { get; }
        [JsonProperty] public ITerm Term { get; }
        [JsonProperty] public IUser Teacher { get; }
    }
}