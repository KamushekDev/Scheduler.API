using System.Threading.Tasks;
using Contracts.Models;

namespace Contracts.Repositories
{
    public interface ILessonsRepository
    {
        public Task<ILesson> GetByName(string name);
        
        //todo: add & update logic
        //Думаю, что тут тебе самому будет удобнее сделать, что тебе надо
    }
}