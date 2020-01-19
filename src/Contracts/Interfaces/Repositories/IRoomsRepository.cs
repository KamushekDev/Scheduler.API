using System.Threading.Tasks;
using Contracts.Interfaces.Models;
using Contracts.Models;

namespace Contracts.Repositories
{
    public interface IRoomsRepository
    {
        public Task<IRoom> GetByName(string name);
        
        //todo: add & update logic
        //Думаю, что тут тебе самому будет удобнее сделать, что тебе надо
    }
}