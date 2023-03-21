using System.Text.Json.Serialization;

namespace Chat.Api.Models
{
    //public class ChatHistory
    //{
    //    public string topic { get; set; }
    //    public DateTime dateTime { get; set; }
    //    public Request request { get; set; }
    //    public Response response { get; set; }
    //}

    public class Response
    {
        [JsonPropertyName("choices")]
        public List<Choice> Choices { get; set; }
        [JsonPropertyName("model")]
        public string Model { get; set; }
        [JsonPropertyName("object")]
        public string @Object { get; set; }
        [JsonPropertyName("created")]
        public double Created { get; set; }
        [JsonPropertyName("usage")]
        public Usage Usage { get; set; }
    }

    public class Choice
    {
        [JsonPropertyName("message")]
        public Message Message { get; set; }
        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }
        [JsonPropertyName("index")]
        public int Index { get; set; }
    }

    public class Usage
    {
        [JsonPropertyName("prompt_tokens")]
        public int PromptTokens { get; set; }
        [JsonPropertyName("completion_tokens")]
        public int CompletionTokens { get; set; }
        [JsonPropertyName("total_tokens")]
        public int TotalTokens { get; set; }
    }

    public class Request
    {
        [JsonPropertyName("model")]
        public string Model { get; set; }
        [JsonPropertyName("temperature")]
        public decimal Temperature { get; set; }
        [JsonPropertyName("messages")]
        public List<Message> Messages { get; set; }
    }

    public class Message
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}