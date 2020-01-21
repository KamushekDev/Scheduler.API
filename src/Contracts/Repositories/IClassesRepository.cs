using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Models;
using Contracts.Parser;

namespace Contracts.Repositories
{
    public interface IClassesRepository
    {
        public Task<IEnumerable<IClass>> GetUserClasses(int userId);
        public Task<int> AddClasses(int userId, string groupName, IEnumerable<ILesson> lessons);
    }
}