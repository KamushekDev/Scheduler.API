using System.Threading.Tasks;
using Contracts.Models;
using Contracts.Repositories;

namespace Data.Dapper.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<IUser> GetById(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}