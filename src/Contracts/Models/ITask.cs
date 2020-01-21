using System;

namespace Contracts.Models
{
    public interface ITask
    {
        int Id { get; }
        DateTime Begin { get; }
        DateTime End { get; }
        string TaskName { get; }
        string LessonName { get; }
        string Description { get; }
    }
}