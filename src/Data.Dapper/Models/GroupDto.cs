using System;
using Contracts.Models;

namespace Data.Dapper.Models
{
    public class GroupDto
    {
        public int groupid { get; set; }
        public string groupname { get; set; }
        public string invitelink { get; set; }

        public Group ToModel() => new Group(groupid, groupname, invitelink);

        public class Group : IGroup
        {
            public int Id { get; }
            public string GroupName { get; }
            public string InviteLink { get; }

            public Group(int id, string groupName, string inviteLink)
            {
                Id = id;
                GroupName = groupName;
                InviteLink = inviteLink;
            }
        }
    }
}