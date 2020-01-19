using System.Collections.Generic;
using Contracts.Enums;
using Contracts.Models;

namespace Contracts.Interfaces.Models
{
    public interface IGroup
    {
        public int Id { get; }
        public string Name { get; }
        public AccessModifier Access { get; }
        public string InviteTag { get; }
        public string Description { get; }
        public ICollection<IUser> Users { get; }
    }
}