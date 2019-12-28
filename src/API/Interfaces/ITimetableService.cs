using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Models;

namespace API.Interfaces
{
    public interface ITimetableService
    {
        Task<List<Class>> GetTimetableByGroups(IEnumerable<string> groups);
    }
}