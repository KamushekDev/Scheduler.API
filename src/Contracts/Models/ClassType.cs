using Contracts.Interfaces.Models;
using Newtonsoft.Json;

namespace Contracts.Models
{
    public class ClassType: IClassType
    {
        public ClassType(string name, string description)
        {
            Name = name;
            Description = description;
        }

        [JsonProperty]
        public string Name { get; }
        [JsonProperty]
        public string Description { get; }
    }
}