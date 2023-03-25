using Domain.Entities;
using Domain.Repositories;

using System.Net;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Domain.Managers
{
    public class ChatManager : IChatManager
    {
        private IChatHistoryRepository _repository;

        private decimal _temperature;
        private HttpClient _httpClient;

        public ChatManager(ILogger<ChatManager> logger,
            IChatHistoryRepository repository,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _repository = repository;
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientFactory.OPEN_AI);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration.GetSection("Chat:ApiKey").Value);
            _temperature = Convert.ToDecimal(configuration.GetSection("Chat:Temperature").Value);
        }

        public async Task<Response> SendMessage(string topic, string content, CancellationToken token)
        {
            var messages = new List<Message>();

            if (topic is not null)
            {
                var chats = await _repository.Get(x => x.Topic == topic, token);
                if (chats.Count > 0)
                {
                    var previous = chats.Select(s => s.Response.Choices
                        .Where(w => w.FinishReason == "stop").Last().Message);
                    messages.AddRange(previous);
                }
            }

            messages.Add(new Message { Role = Role.user.ToString(), Content = content.ToString() });

            var request = new Request
            {
                Model = Constants.GPT_3_5_TURBO,
                Temperature = _temperature,
                Messages = messages
            };

            var jsonRequest = JsonSerializer.Serialize(request);
            var payload = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync("chat/completions", payload, token);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseBody = responseMessage.Content.ReadAsStringAsync().Result;
                var response = JsonSerializer.Deserialize<Response>(responseBody);

                await _repository.Create(new ChatHistory
                {
                    //StatusCode = responseMessage.StatusCode,
                    StatusCode = HttpStatusCode.OK,
                    DateTime = DateTime.UtcNow,
                    Topic = topic,
                    Request = request,
                    Response = response
                }, token);

                return response;
            }
            else
            {
#if DEBUG
                var displayMessage = $"{responseMessage.StatusCode} : {responseMessage.ReasonPhrase}";
                throw new Exception(displayMessage);
#else
                throw new Exception(":( Something went wrong !");
#endif

            }

        }
    }
}