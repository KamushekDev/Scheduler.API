using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using Contracts.Models;
using Data.Dapper.Interfaces;

namespace API.Services
{
    public class TimetableService: ITimetableService
    {
        private readonly ITimetableRepository _timetableRepository;
        
        public TimetableService(ITimetableRepository timetableRepository)
        {
            _timetableRepository = timetableRepository;
        }
        
        public async Task<List<Class>> GetTimetableByGroups(IEnumerable<string> groups)
        {
            if (groups is null)
            {
                return new List<Class>();
            }

            var result = await _timetableRepository.GetTimetableByGroups(groups);
            return result.ToList();
        }
    }
}