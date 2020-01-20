using System;

namespace Contracts.Models
{
    public interface IClass
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
    }
}