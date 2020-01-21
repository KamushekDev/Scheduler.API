using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Models;
using Contracts.Repositories;
using Data.Dapper.Models;

namespace Data.Dapper.Repositories
{
    public class CredentialRepository : ICredentialRepository
    {
        private readonly DatabaseAccess _databaseAccess;

        public CredentialRepository(DatabaseAccess databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }

        public async Task<ICredential> GetByProvider(string provider, int providerUserId)
        {
            const string query =
                @"select user_id as userId,
                         provider as provider,
                         access_token as accessToken,
                         date_expired as dateExpired,
                         provider_user_id as providerUserId
                  from credentials
                  where provider = @provider and provider_user_id = @providerUserId;";
            var response = await _databaseAccess.ExecuteQueryFirstOrDefaultAsync<CredentialDto>(query,
                new {provider = provider, providerUserId = providerUserId});
            return response?.ToModel();
        }

        public async Task<bool> AddCredential(string provider, int providerUserId, int userId, string accessToken,
            DateTime dateExpired)
        {
            const string query =
                @"insert into credentials (user_id, provider, access_token, date_expired, provider_user_id) 
                         values (@userId, @provider, @accessToken, @dateExpired, @providerUserId)";
            var response = await _databaseAccess.ExecuteAsync(query,
                new
                {
                    provider = provider,
                    providerUserId = providerUserId,
                    userId = userId,
                    accessToken = accessToken,
                    dateExpired = dateExpired
                });

            return response == 1;
        }
    }
}