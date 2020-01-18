using System;

namespace Contracts.Models
{
    public interface ITask
    {
        public int Id { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AccessModifier Access { get; set; }
        public IClass EndClass { get; set; }
    }
}