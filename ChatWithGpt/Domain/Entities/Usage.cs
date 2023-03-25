namespace Domain.Entities
{
    public class Usage : BaseEntity
    {
        public int TotalTokens { get; set; }
        public int PromptTokens { get; set; }
        public int CompletionTokens { get; set; }
    }
}