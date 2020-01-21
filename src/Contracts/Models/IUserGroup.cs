using System;

namespace Contracts.Models
{
    public interface IUserGroup
    {
        public int Id { get; }
        public string GroupName { get; }
        public UserRole UserRole { get; }
        public DateTime EntryDate { get; }
        public string InviteLink { get; }
    }
}