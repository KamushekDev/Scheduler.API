using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Models;
using Dapper;
using Data.Dapper.Interfaces;

namespace Data.Dapper.Repositories
{
    public class TimetableRepository : ITimetableRepository
    {
        private BaseDataAcсess _baseDataAcсess;

        public TimetableRepository(BaseDataAcсess baseDataAcсess)
        {
            _baseDataAcсess = baseDataAcсess;
        }

        public Task<IEnumerable<Class>> GetTimetableByGroupId(int groupId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("GroupId", groupId);
            return _baseDataAcсess.ExecuteStoredProcedureWhichReturnsCollectionAsync<Class>(
                StoredProcedures.GetTimetableProcedure, parameters);
        }
    }
}