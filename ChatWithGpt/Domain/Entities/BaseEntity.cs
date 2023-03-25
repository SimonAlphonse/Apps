using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class BaseEntity
    {
        [JsonIgnore]
        public int Id { get; set; }
    }
}