using Domain.Entities;

namespace Domain.Managers
{
    public interface IChatManager
    {
        Task<Response> SendMessage(string topic, string content, CancellationToken token);
    }
}