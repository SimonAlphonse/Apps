using Domain.Entities;
using Domain.Repositories;
using Application.Interfaces;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ChatHistoryRepository : IRepository<ChatHistory>
    {
        private readonly IApplicationDbContext _dbContext;

        public ChatHistoryRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<ChatHistory>> Get(Expression<Func<ChatHistory, bool>> expression, CancellationToken token)
        {
            return await _dbContext.ChatHistory
               .Include(i => i.Request).ThenInclude(i => i.Messages)
               .Include(i => i.Response).ThenInclude(i => i.Choices).ThenInclude(i => i.Message)
               .Include(i => i.Response).ThenInclude(i => i.Usage).Where(expression).ToListAsync(token);
        }

        public async Task<ChatHistory> Create(ChatHistory entity, CancellationToken token)
        {
            var record = _dbContext.ChatHistory.Add(entity).Entity;
            await _dbContext.SaveChangesAsync(token);
            return record;
        }

        public async Task<ChatHistory> Update(ChatHistory entity, CancellationToken token)
        {
            var record = _dbContext.ChatHistory.Update(entity).Entity;
            await _dbContext.SaveChangesAsync(token);
            return record;
        }

        public async Task<bool> Delete(int id, CancellationToken token)
        {
            var chat = await _dbContext.ChatHistory.FindAsync(new object[] { id }, token: token);
            if (chat == null) return false;

            _dbContext.ChatHistory.Remove(chat);
            await _dbContext.SaveChangesAsync(token);
            return true;
        }
    }
}