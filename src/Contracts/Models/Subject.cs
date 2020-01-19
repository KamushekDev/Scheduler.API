using Contracts.Parser;
using Newtonsoft.Json;

namespace Contracts.Models
{
    public class Subject: ISubject
    {
        public Subject(string name)
        {
            Name = name;
        }

        [JsonProperty]
        public string Name { get; }
    }
}