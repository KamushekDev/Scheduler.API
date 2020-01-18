using System;

namespace Contracts.Models
{
    public interface ITerm
    {
        public int Id { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public string Description { get; }
    }
}