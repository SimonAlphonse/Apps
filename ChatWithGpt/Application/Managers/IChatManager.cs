using Domain.Entities;

namespace Applicatoin.Managers
{
    public interface IChatManager
    {
        Task<Response> SendMessage(string title, string context, string content, CancellationToken token);
    }
}