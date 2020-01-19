using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Interfaces.Models;
using Contracts.Models;

namespace Contracts.Repositories
{
    public interface IGroupsRepository
    {
        public Task<IGroup> GetById(int id);
        public Task<ICollection<IGroup>> GetUserGroups(IUser user);
        public Task<ICollection<IGroup>> GetUserGroups(int userId);
        public Task<IGroup> GetByInviteTag(string inviteTag);
        
        //todo: add & update logic
        //Думаю, что тут тебе самому будет удобнее сделать, что тебе надо
    }
}