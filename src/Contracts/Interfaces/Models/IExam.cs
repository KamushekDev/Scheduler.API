using System;
using Contracts.Enums;
using Contracts.Models;

namespace Contracts.Interfaces.Models
{
    public interface IExam
    {
        public int Id { get; }
        public string Name { get; }
        public DateTime DateTime { get; }
        public AccessModifier Access { get; }
        public IGroup Group { get; }
        public ILesson Lesson { get; }
        public IUser Teacher{ get; }
    }
}