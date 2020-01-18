using System;

namespace Contracts.Models
{
    public interface IExam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public AccessModifier Access { get; set; }
        public IGroup Group { get; set; }
        public ILesson Lesson { get; set; }
        public IUser Teacher{ get; set; }
    }
}