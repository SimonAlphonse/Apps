namespace Domain
{
    public class Constants
    {
        public const string GPT_3_5_TURBO = "gpt-3.5-turbo";
        public const string OPEN_AI_URI = "https://api.openai.com/v1/";

        public class HttpClientFactory
        {
            public const string OPEN_AI = "OpenAi";
        }
    }
    
    public enum Role
    {
        user,
        assistant,
    }
}