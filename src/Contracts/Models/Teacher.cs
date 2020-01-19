using System.Collections.Generic;
using Contracts.Interfaces.Models;
using Newtonsoft.Json;

namespace Contracts.Models
{
    public class User: IUser
    {
        public User(int id, string name, string surname, string patronymic, string phone, string email, ICollection<ICredential> credentials, ICollection<IGroup> groups)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Phone = phone;
            Email = email;
            Credentials = credentials;
            Groups = groups;
        }

        [JsonProperty]
        public int Id { get; }
        [JsonProperty]
        public string Name { get; }
        [JsonProperty]
        public string Surname { get; }
        [JsonProperty]
        public string Patronymic { get; }
        [JsonProperty]
        public string Phone { get; }
        [JsonProperty]
        public string Email { get; }
        [JsonProperty]
        public ICollection<ICredential> Credentials { get; }
        [JsonProperty]
        public ICollection<IGroup> Groups { get; }
    }
}