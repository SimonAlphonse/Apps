namespace Domain.Entities
{
    public class Response : BaseEntity
    {
        public string Model { get; set; }
        public string Object { get; set; }
        public double Created { get; set; }
        public Usage Usage { get; set; }
        public List<Choice> Choices { get; set; }
    }
}