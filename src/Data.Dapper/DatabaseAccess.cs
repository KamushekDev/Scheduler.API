using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Helpers;
using Contracts.Models;
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

        public async Task<NpgsqlConnection> GetConnectionAsync()
        {
            var connection = new NpgsqlConnection(_databaseConfiguration.ConnectionString);

            await connection.OpenAsync();

            return connection;
        }

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string query,
            object parameters = null)
        {
            var connection = await GetConnectionAsync();
            await using (connection.ConfigureAwait(false))
            {
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
            var connection = await GetConnectionAsync();
            await using (connection.ConfigureAwait(false))
            {
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
            var connection = await GetConnectionAsync();
            await using (connection.ConfigureAwait(false))
            {
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
        
        public async Task<int> ExecuteAsync(string query,
            params NpgsqlParameter[] parameters) {
            var connection = await GetConnectionAsync();

            await using (var cmd = new NpgsqlCommand(query, connection))
            {
                cmd.Parameters.AddRange(parameters);
                
                return await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}