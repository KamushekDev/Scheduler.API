using System;

namespace Contracts.Models
{
    public interface ICredential
    {
        public string Provider { get; }
        public string AccessToken { get; }
        public DateTime DateExpired { get; }
    }
}