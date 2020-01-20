using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Models;

namespace Contracts.Repositories
{
    public interface IClassesRepository
    {
        public Task<IEnumerable<IClass>> GetUserClasses(int userId);
    }
}