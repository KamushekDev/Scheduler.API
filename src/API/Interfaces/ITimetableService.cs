using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Interfaces.Models;

namespace API.Interfaces
{
    public interface ITimetableService
    {
        public Task<List<IClass>> GetClassesByGroup(int group);
    }
}