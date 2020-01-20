using System;

namespace Contracts.Models
{
    public interface ITask
    {
        public int Id { get; }
        public DateTime Begin { get; }
        public DateTime End { get; }
        public string TaskName { get; }
        public string LessonName { get; }
        public string Description { get; }
    }
}