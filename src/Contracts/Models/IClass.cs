using System;

namespace Contracts.Models
{
    public interface IClass
    {
        public int Id { get; set; }
        public TimeSpan StartTime { get; set; }
        public AccessModifier Access { get; set; }
        public int Duration { get; set; }
        public ILesson Lesson { get; set; }
        public IRoom Room { get; set; }
        public IGroup Group { get; set; }
        public IClassType ClassType { get; set; }
        public ITerm Term { get; set; }
        public IUser Teacher { get; set; }
    }
}