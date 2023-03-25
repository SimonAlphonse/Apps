using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Response : BaseEntity
    {
        [JsonPropertyName("model")]
        public string Model { get; set; }
        [JsonPropertyName("object")]
        public string Object { get; set; }
        [JsonPropertyName("created")]
        public double Created { get; set; }
        [JsonPropertyName("usage")]
        public Usage Usage { get; set; }
        [JsonPropertyName("choices")]
        public List<Choice> Choices { get; set; }
    }
}