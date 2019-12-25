using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Models;

namespace Data.Dapper.Interfaces
{
    public interface ITimetableRepository
    {
        Task<IEnumerable<Class>> GetTimetableByGroupId(int groupId);
    }
}