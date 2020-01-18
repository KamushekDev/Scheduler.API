using Newtonsoft.Json;

namespace API.Models
{
    public class AuthSettings
    {
        public JWTToken TokenSettings { get; set; }
        
        public VkAuthSettings VkSettings { get; set; }
    }
}