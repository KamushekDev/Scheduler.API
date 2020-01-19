using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Models;

namespace Contracts.Repositories
{
    public interface IClassesRepository
    {
        public Task<IClass> GetById(int id);

        public Task<ICollection<IClass>> GetGroupClasses(IGroup group)
            => GetGroupClasses(group.Id);

        public Task<ICollection<IClass>> GetGroupClasses(int groupId);

        public Task<ICollection<IClass>> GetClassesByTerm(ITerm term)
            => GetClassesByTerm(term.Id);

        public Task<ICollection<IClass>> GetClassesByTerm(int termId);

        public Task<ICollection<IClass>> GetGroupClassesByTerm(IGroup group, ITerm term)
            => GetGroupClassesByTerm(group.Id, term.Id);

        public Task<ICollection<IClass>> GetGroupClassesByTerm(IGroup group, int termId)
            => GetGroupClassesByTerm(group.Id, termId);

        public Task<ICollection<IClass>> GetGroupClassesByTerm(int groupId, ITerm term)
            => GetGroupClassesByTerm(groupId, term.Id);

        public Task<ICollection<IClass>> GetGroupClassesByTerm(int groupId, int termId);
        
        //todo: add & update logic
        //Думаю, что тут тебе самому будет удобнее сделать, что тебе надо
    }
}