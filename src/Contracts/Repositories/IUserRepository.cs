using System.Threading.Tasks;
using Contracts.Models;

namespace Contracts.Repositories
{
    public interface IUserRepository
    {
        public Task<IUser> GetById(int userId);
        
    }
}