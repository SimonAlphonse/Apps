using Application.Managers;

using Microsoft.AspNetCore.Mvc;
using Presentation.Models;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private IChatManager _chatManager;

        public ChatController(ILogger<ChatController> logger, IChatManager chatManager)
        {
            _logger = logger;
            _chatManager = chatManager;
        }

        [HttpPost(Name = "[action]")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageParams messageParams, CancellationToken token)
        {
            var response = await _chatManager.SendMessage(messageParams.Title, messageParams.Context, messageParams.Content, token);

            if (response != null)
            {
                return Ok(response.Choices.Last().Message.Content);
            }

            return NotFound(string.Empty);
        }
    }
}