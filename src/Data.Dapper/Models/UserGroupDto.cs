using System;
using Contracts.Models;

namespace Data.Dapper.Models
{
    public class UserGroupDto
    {
        public int groupid { get; set; }
        public string groupname { get; set; }
        public UserRole userrole { get; set; }
        public DateTime entrydate { get; set; }
        public string invitelink { get; set; }

        public UserGroup ToModel() => new UserGroup(groupid, groupname, userrole, entrydate, invitelink);

        public class UserGroup : IUserGroup
        {
            public int Id { get; }
            public string GroupName { get; }
            public UserRole UserRole { get; }
            public DateTime EntryDate { get; }
            public string InviteLink { get; }

            public UserGroup(int id, string groupName, UserRole userRole, DateTime entryDate, string inviteLink)
            {
                Id = id;
                GroupName = groupName;
                UserRole = userRole;
                EntryDate = entryDate;
                InviteLink = inviteLink;
            }
        }
    }
}