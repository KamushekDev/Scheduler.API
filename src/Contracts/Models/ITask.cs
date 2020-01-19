using System;

namespace Contracts.Models
{
    public interface ITask
    {
        public int Id { get; }
        public DateTime DateBegin { get; }
        public DateTime DateEnd { get; }
        public string Name { get; }
        public string Description { get; }
        public AccessModifier Access { get; }
        public IClass EndClass { get; }
    }
}