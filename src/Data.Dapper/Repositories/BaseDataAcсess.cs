using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Helpers;
using Dapper;

namespace Data.Dapper.Repositories
{
    public class BaseDataAcсess
    {
        private readonly string _connectionString;
        private readonly int _timeoutSeconds;
        

        public BaseDataAcсess()
        {
            _connectionString = DatabaseConfiguration.ConnectionString;
            _timeoutSeconds = DatabaseConfiguration.TimeoutSeconds;
        }
        
        public async Task<IEnumerable<T>> ExecuteStoredProcedureWhichReturnsCollectionAsync<T>(string procedure,
            DynamicParameters parameters = null)
        {
            using var cts = new CancellationTokenSource(_timeoutSeconds * 1000);
            var cd = new CommandDefinition(procedure, parameters, commandType: CommandType.StoredProcedure, cancellationToken: cts.Token, commandTimeout: _timeoutSeconds);
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(cts.Token).ConfigureAwait(false);
            try
            {
                return await connection.QueryAsync<T>(cd).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception in {procedure}", ex);
            }
        }

        public async Task ExecuteVoidStoredProcedureAsync(string procedure, DynamicParameters parameters = null)
        {
            using var cts = new CancellationTokenSource(_timeoutSeconds * 1000);
            var cd = new CommandDefinition(procedure, parameters, commandType: CommandType.StoredProcedure, cancellationToken: cts.Token, commandTimeout: _timeoutSeconds);
            await using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync(cts.Token).ConfigureAwait(false);
            try
            {
                await connection.ExecuteAsync(cd).ConfigureAwait(false);
            }
            catch (SqlException ex)
            {
                throw new Exception($"Exception in {procedure}", ex);
            }
        }
        
        public async Task<T> ExecuteStoredProcedureWhichReturnsSingleAsync<T>(string procedure,
            DynamicParameters parameters = null)
        {
            var result = await ExecuteStoredProcedureWhichReturnsCollectionAsync<T>(procedure, parameters)
                .ConfigureAwait(false);
            return result.FirstOrDefault();
        }
    }
}