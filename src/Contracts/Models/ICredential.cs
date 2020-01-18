using System;

namespace Contracts.Models
{
    public interface ICredential
    {
        public string Provider { get; set; }
        public string AccessToken { get; set; }
        public DateTime DateExpired { get; set; }
    }
}