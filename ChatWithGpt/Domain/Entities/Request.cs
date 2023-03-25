using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Request : BaseEntity
    {
        [JsonPropertyName("model")]
        public string Model { get; set; }
        [JsonPropertyName("temperature")]
        public decimal Temperature { get; set; }
        [JsonPropertyName("messages")]
        public List<Message> Messages { get; set; }
    }
}