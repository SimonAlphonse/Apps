using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Chat.Api.Models;
using Chat.Api.Repositories;

namespace Chat.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ChatRepository _chatRepository;
        private readonly HttpClient _httpClient;
        private decimal _temperature;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory, ChatRepository chatRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _chatRepository = chatRepository;
            _temperature = Convert.ToDecimal(_configuration.GetSection("Chat:Temperature").Value);
            _httpClient = httpClientFactory.CreateClient("OpenAi");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _configuration.GetSection(@"Chat:Bearer").Value);
        }

        [HttpPost(Name = "[action]")]
        public async Task<IActionResult> SendMessage(string topic, string content, CancellationToken token)
        {
            var messages = new List<Message>();

            if (topic is not null)
            {
                var chats = await _chatRepository.GetByTopic(topic);
                if (chats.Count > 0)
                {
                    var oldMessages = chats.Select(s => s.response.choices
                        .Where(w => w.finish_reason == "stop").Last().message);
                    messages.AddRange(oldMessages);
                }
            }

            messages.Add(new Message { role = Role.user.ToString(), content = content.ToString() });

            var request = new Request
            {
                model = Model.GPT_3_5_TURBO,
                temperature = _temperature,
                messages = messages
            };

            var payload = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync("chat/completions", payload, token);

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseBody = responseMessage.Content.ReadAsStringAsync().Result;
                var response = JsonSerializer.Deserialize<Response>(responseBody);
                var choice = response!.choices.Last();

                _chatRepository.Insert(new Chat
                {
                    dateTime = DateTime.UtcNow,
                    topic = topic,
                    request = request,
                    response = response
                });

                return Ok(choice.message.content);
            }

            return NotFound(string.Empty);
        }
    }
}