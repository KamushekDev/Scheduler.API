using System.Text.Json.Serialization;

namespace API.Models
{
    public class VkAccessTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }
    }
}