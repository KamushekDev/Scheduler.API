using System;

namespace Contracts.Interfaces.Models
{
    public interface ICredential
    {
        public string Provider { get; }
        public string AccessToken { get; }
        public DateTime DateExpired { get; }
    }
}