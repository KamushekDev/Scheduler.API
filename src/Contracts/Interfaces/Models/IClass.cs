using System;
using Contracts.Enums;

namespace Contracts.Interfaces.Models
{
    public interface IClass
    {
        public int Id { get; }
        public TimeSpan StartTime { get; }
        public AccessModifier Access { get; }
        public int Duration { get; }
        public ILesson Lesson { get; }
        public IRoom Room { get; }
        public IGroup Group { get; }
        public IClassType ClassType { get; }
        public ITerm Term { get; }
        public IUser Teacher { get; }
    }
}