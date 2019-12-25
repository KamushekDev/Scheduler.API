using Contracts.Enums;

namespace Contracts.Models
{
    public class Lesson
    {
        public LessonNames Name { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
    }
}