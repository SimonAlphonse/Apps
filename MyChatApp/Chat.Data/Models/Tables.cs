using System.ComponentModel.DataAnnotations;

namespace Chat.Data.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }

    public class ChatHistory : BaseEntity
    {
        public string Topic { get; set; }
        public DateTime DateTime { get; set; }
        public Request Request { get; set; }
        public Response Response { get; set; }
    }

    public class Response : BaseEntity
    {
        public List<Choice> Choices { get; set; }
        public string Model { get; set; }
        public string Object { get; set; }
        public double Created { get; set; }
        public Usage Usage { get; set; }
    }

    public class Choice : BaseEntity
    {
        public Message Message { get; set; }
        public string FinishReason { get; set; }
        public int Index { get; set; }
    }

    public class Usage : BaseEntity
    {
        public int PromptTokens { get; set; }
        public int CompletionTokens { get; set; }
        public int TotalTokens { get; set; }
    }

    public class Request : BaseEntity
    {
        public string Model { get; set; }
        public decimal Temperature { get; set; }
        public List<Message> Messages { get; set; }
    }

    public class Message : BaseEntity
    {
        public string Role { get; set; }
        public string Content { get; set; }
    }

    public class RequestMessage : Message
    {

    }

    public class ResponseMessage : Message
    {

    }
}