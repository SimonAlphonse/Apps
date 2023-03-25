using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IRepository<TEntity>
    {
        Task<IReadOnlyList<ChatHistory>> Get(Expression<Func<ChatHistory, bool>> expression, CancellationToken token);
        Task<ChatHistory> Create(ChatHistory entity, CancellationToken token);
        Task<ChatHistory> Update(ChatHistory entity, CancellationToken token);
        Task<bool> Delete(int id, CancellationToken token);
    }
}