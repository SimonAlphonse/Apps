using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Message : BaseEntity
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}