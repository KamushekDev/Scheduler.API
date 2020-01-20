using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Models;

namespace Contracts.Repositories
{
    public interface ITaskRepository
    {
        public Task<IEnumerable<ITask>> GetUserTasks(int userId);
    }
}