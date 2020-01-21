using System;
using System.Threading.Tasks;
using Contracts.Models;

namespace Contracts.Repositories
{
    public interface ICredentialRepository
    {
        public Task<ICredential> GetByProvider(string provider, int providerUserId);
        public Task<bool> AddCredential(string provider, int providerUserId, int userId, string accessToken, DateTime dateExpired);
    }
}