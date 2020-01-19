using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Interfaces.Models;
using Contracts.Models;

namespace Contracts.Repositories
{
    public interface ITermsRepository
    {
        public Task<ITerm> GetById(string id);
        public Task<IEnumerable<ITerm>> GetTermIncludesDate(DateTime date);
        
        //todo: add & update logic
        //Думаю, что тут тебе самому будет удобнее сделать, что тебе надо
    }
}