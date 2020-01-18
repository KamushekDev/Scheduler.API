using Newtonsoft.Json;

namespace API.Models
{
    internal class VkAccessTokenResponse
    {
        [JsonProperty("access_token")]
        internal string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        internal int ExpiresIn { get; set; }

        [JsonProperty("user_id")]
        internal int UserId { get; set; }
    }
}