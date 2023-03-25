namespace Domain.Entities
{
    public class Message : BaseEntity
    {
        public string Role { get; set; }
        public string Content { get; set; }
    }
}