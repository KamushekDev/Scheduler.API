using System.Text.Json.Serialization;

namespace API.Models
{
    public class VkGetResponse
    {
        [JsonPropertyName("response")]
        public VkProfile[] Response { get; set; }
    }
}