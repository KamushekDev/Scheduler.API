using System.Threading.Tasks;
using Contracts.Models;

namespace Contracts.Repositories
{
    public interface IUsersRepository
    {
        public Task<IUser> GetById(int id);
        public Task<IUser> GetByPhone(string phone);
        public Task<IUser> GetByEmail(string email);
        
        //todo: add & update logic
        //Думаю, что тут тебе самому будет удобнее сделать, что тебе надо
    }
}