using System;

namespace Contracts.Models
{
    public interface IClass
    {
        int Id { get; }
        string LessonName { get; }
        string RoomName { get; }
        TimeSpan StartTime { get; }
        int Duration { get; }
        string ClassTypeName { get; }
        string GroupName { get; }
        IUser Teacher { get; }
        WeekType WeekType { get; }
        int DayNumber { get; }
    }
}