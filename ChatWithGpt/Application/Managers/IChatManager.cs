using Domain.Entities;

namespace Domain.Managers
{
    public interface IChatManager
    {
        Task<Response> SendMessage(string title, string content, CancellationToken token);
    }
}