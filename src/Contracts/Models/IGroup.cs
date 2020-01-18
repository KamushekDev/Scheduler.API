using System.Collections.Generic;

namespace Contracts.Models
{
    public interface IGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AccessModifier Access { get; set; }
        public string InviteTag { get; set; }
        public string Description { get; set; }
        public ICollection<IUser> Users { get; set; }
    }
}