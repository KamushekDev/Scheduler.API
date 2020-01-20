using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Helpers;
using Dapper;
using Npgsql;

namespace Data.Dapper
{
    public class BaseDataAcсess
    {
        private readonly DatabaseConfiguration _databaseConfiguration;

        public BaseDataAcсess(DatabaseConfiguration databaseConfiguration)
        {
            _databaseConfiguration = databaseConfiguration;
        }

        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_databaseConfiguration.ConnectionString);
        }

        public async Task<IEnumerable<T>> ExecuteQuery<T>(string query,
            object parameters = null)
        {
            var connection = GetConnection();
            await using (connection.ConfigureAwait(false))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                try
                {
                    return await connection.QueryAsync<T>(query, parameters).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Exception in {nameof(ExecuteQuery)}", ex);
                }
            }
        }
    }
}