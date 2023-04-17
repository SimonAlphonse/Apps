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
        private ILogger<ChatManager> _logger;
        private IChatHistoryRepository _repository;

        private decimal _temperature;
        private HttpClient _httpClient;

        public ChatManager(ILogger<ChatManager> logger,
            IHttpClientFactory httpClientFactory,
            IChatHistoryRepository repository,
            IConfiguration configuration)
        {
            _logger = logger;
            _repository = repository;
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientFactory.OPEN_AI);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration.GetSection("Chat:ApiKey").Value);
            _temperature = Convert.ToDecimal(configuration.GetSection("Chat:Temperature").Value);
        }

        public async Task<Response> SendMessage(string title, string context, string content, CancellationToken token)
        {
            var messages = new List<Message>();

            if (title is not null)
            {
                var chats = await _repository.Get(x => x.Title == title && x.StatusCode == HttpStatusCode.OK, token);
                foreach (var chat in chats.OrderBy(o => o.Id))
                {
                    messages.Add(chat.Request.Messages.Last());
                    messages.Add(chat.Response.Choices.Last().Message);
                }
            }

            if(!string.IsNullOrWhiteSpace(context) && !messages.Any())
            {
                messages.Add(new Message { Role = Role.system.ToString(), Content = context });
            }

            messages.Add(new Message { Role = Role.user.ToString(), Content = content });

            var request = new Request
            {
                Model = Constants.GPT_3_5_TURBO,
                Temperature = _temperature,
                Messages = messages,
            };

            var jsonRequest = JsonSerializer.Serialize(request);
            var payload = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync("chat/completions", payload, token);
            var responseBody = responseMessage.Content.ReadAsStringAsync(token).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                var response = JsonSerializer.Deserialize<Response>(responseBody);

                await _repository.Create(new ChatHistory
                {
                    StatusCode = responseMessage.StatusCode,
                    DateTime = DateTime.UtcNow,
                    Title = title,
                    Request = request,
                    Response = response
                }, token);

                return response;
            }
            else
            {
                _logger.LogInformation(responseBody);
                throw new Exception($"{responseMessage.StatusCode} {responseMessage.ReasonPhrase} : {responseBody}");
            }
        }
    }
}