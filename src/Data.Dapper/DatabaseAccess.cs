using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Helpers;
using Dapper;
using Npgsql;

namespace Data.Dapper
{
    public class DatabaseAccess
    {
        private readonly DatabaseConfiguration _databaseConfiguration;

        public DatabaseAccess(DatabaseConfiguration databaseConfiguration)
        {
            _databaseConfiguration = databaseConfiguration;
        }

        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_databaseConfiguration.ConnectionString);
        }

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string query,
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
                    throw new Exception($"Exception in {nameof(ExecuteQueryAsync)}", ex);
                }
            }
        }

        public async Task<T> ExecuteQueryFirstOrDefaultAsync<T>(string query,
            object parameters = null)
        {
            var connection = GetConnection();
            await using (connection.ConfigureAwait(false))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                try
                {
                    return await connection.QueryFirstOrDefaultAsync<T>(query, parameters).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Exception in {nameof(ExecuteQueryAsync)}", ex);
                }
            }
        }

        public async Task<int> ExecuteAsync(string query,
            object parameters = null)
        {
            var connection = GetConnection();
            await using (connection.ConfigureAwait(false))
            {
                await connection.OpenAsync().ConfigureAwait(false);
                try
                {
                    return await connection.ExecuteAsync(query, parameters).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Exception in {nameof(ExecuteQueryAsync)}", ex);
                }
            }
        }
    }
}