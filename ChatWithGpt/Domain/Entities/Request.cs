namespace Domain.Entities
{
    public class Request : BaseEntity
    {
        public string Model { get; set; }
        public decimal Temperature { get; set; }
        public List<Message> Messages { get; set; }
    }
}