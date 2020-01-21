using System.Threading.Tasks;
using Contracts.Models;

namespace Contracts.Repositories
{
    public interface IUserRepository
    {
        public Task<IUser> GetById(int userId);

        public Task<bool> UpdateUser(int userId, string name, string surname);
        
        public Task<int> AddUser(string name,
            string surname,
            string patronymic = null,
            string phone = null,
            string email = null);

        public Task<bool> JoinGroup(int userId, int groupId);
    }
}