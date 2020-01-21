using System;
using Contracts.Models;

namespace Data.Dapper.Models
{
    public class CredentialDto
    {
        public int userid { get; set; }
        public string provider { get; set; }
        public string accesstoken { get; set; }
        public DateTime dateexpired { get; set; }
        public string provideruserid { get; set; }

        public Credential ToModel() => new Credential(userid, provider, provideruserid, accesstoken, dateexpired);

        public class Credential : ICredential
        {
            public int UserId { get; }
            public string Provider { get; }
            public string ProviderUserId { get; }
            public string AccessToken { get; }
            public DateTime DateExpired { get; }

            public Credential(int userId, string provider, string providerUserId, string accessToken,
                DateTime dateExpired)
            {
                UserId = userId;
                Provider = provider;
                ProviderUserId = providerUserId;
                AccessToken = accessToken;
                DateExpired = dateExpired;
            }
        }
    }
}