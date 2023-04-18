using Domain.Entities;

namespace Application.Managers
{
    public interface IChatManager
    {
        Task<Response> SendMessage(string title, string context, string content, CancellationToken token);
    }
}