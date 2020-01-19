using System;
using Contracts.Interfaces.Models;
using Newtonsoft.Json;

namespace Contracts.Models
{
    public class Term: ITerm
    {
        public Term(int id, DateTime startDate, DateTime endDate, string description)
        {
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
        }

        [JsonProperty]
        public int Id { get; }
        [JsonProperty]
        public DateTime StartDate { get; }
        [JsonProperty]
        public DateTime EndDate { get; }
        [JsonProperty]
        public string Description { get; }
    }
}