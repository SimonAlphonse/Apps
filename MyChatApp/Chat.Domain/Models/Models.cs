namespace ChatCompletion.Core.Models
{
    public class ChatHistory
    {
        public string Topic { get; set; }
        public DateTime DateTime { get; set; }
        public Request Request { get; set; }
        public Response Response { get; set; }
    }

    public class Response
    {
        public List<Choice> Choices { get; set; }
        public string Model { get; set; }
        public string Object { get; set; }
        public double Created { get; set; }
        public Usage Usage { get; set; }
    }

    public class Choice
    {
        public Message Message { get; set; }
        public string FinishReason { get; set; }
        public int Index { get; set; }
    }

    public class Usage
    {
        public int PromptTokens { get; set; }
        public int CompletionTokens { get; set; }
        public int TotalTokens { get; set; }
    }

    public class Request
    {
        public string Model { get; set; }
        public decimal Temperature { get; set; }
        public List<Message> Messages { get; set; }
    }

    public class Message
    {
        public Role Role { get; set; }
        public string Content { get; set; }
    }
}