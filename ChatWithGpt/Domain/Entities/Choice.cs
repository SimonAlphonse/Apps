namespace Domain.Entities
{
    public class Choice : BaseEntity
    {
        public int Index { get; set; }
        public int ResponseId { get; set; }
        public string FinishReason { get; set; }
        public Message Message { get; set; }
    }
}