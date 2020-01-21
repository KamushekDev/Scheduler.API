using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Models;

namespace Contracts.Repositories
{
    public interface IGroupRepository
    {
        public Task<IGroup> GetById(int groupId);
        public Task<IGroup> GetByInviteLink(string inviteLink);
        public Task<IEnumerable<IGroup>> GetUserGroups(int userId);
        public Task<bool> MakeGroupPublic(int groupId);
        public Task<int> CreateGroup(int userId, string name, string description = null);
        public Task<IEnumerable<IGroup>> GetPublicGroupsWithoutUser(int userId);
    }
}