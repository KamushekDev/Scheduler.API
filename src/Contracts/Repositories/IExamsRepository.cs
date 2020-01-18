using System.Threading.Tasks;
using Contracts.Models;

namespace Contracts.Repositories
{
    public interface IExamsRepository
    {
        public Task<IExam> GetById(int id);
        
        //todo: add & update logic
        //Думаю, что тут тебе самому будет удобнее сделать, что тебе надо
    }
}