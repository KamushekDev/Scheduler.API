using System.Collections.Generic;
using Contracts.Enums;
using Contracts.Interfaces.Models;
using Newtonsoft.Json;

namespace Contracts.Models
{
    public class Group: IGroup
    {
        public Group(int id, string name, AccessModifier access, string inviteTag, string description, ICollection<IUser> users)
        {
            Id = id;
            Name = name;
            Access = access;
            InviteTag = inviteTag;
            Description = description;
            Users = users;
        }

        [JsonProperty]
        public int Id { get; }
        [JsonProperty]
        public string Name { get; }
        [JsonProperty]
        public AccessModifier Access { get; }
        [JsonProperty]
        public string InviteTag { get; }
        [JsonProperty]
        public string Description { get; }
        [JsonProperty]
        public ICollection<IUser> Users { get; }
    }
}