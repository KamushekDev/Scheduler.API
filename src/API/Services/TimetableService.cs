using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using Contracts.Interfaces.Models;
using Contracts.Interfaces.Repositories;

namespace API.Services
{
    public class TimetableService: ITimetableService
    {
        private readonly IClassesRepository _classesRepository;
        
        public TimetableService(IClassesRepository classesRepository)
        {
            _classesRepository = classesRepository;
        }
        
        public async Task<List<IClass>> GetClassesByGroup(int group)
        {
            if (group == 0)
            {
                return new List<IClass>();
            }

            var result = await _classesRepository.GetGroupClasses(group);
            return result.ToList();
        }
        
    }
}