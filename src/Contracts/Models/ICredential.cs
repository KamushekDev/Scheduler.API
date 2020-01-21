using System;

namespace Contracts.Models
{
    public interface ICredential
    {
        int UserId { get; }
        string Provider { get; }
        string ProviderUserId { get; }
        string AccessToken { get; }
        DateTime DateExpired { get; }
    }
}