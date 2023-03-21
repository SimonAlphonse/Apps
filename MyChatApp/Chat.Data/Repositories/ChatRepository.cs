using ChatCompletion.Api.Models;
using Microsoft.EntityFrameworkCore;
using ChatCompletion.Api.DbContexts;

namespace ChatCompletion.Api.Repositories
{
    public class ChatRepository
    {
        private readonly ChatDbContext _dbContext;

        public ChatRepository(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ChatHistory>> GetByTopic(string topic)
        {
            return await _dbContext.Chat.Where(w => w.topic == topic)
                .Include(c => c.request).ThenInclude(r => r.messages)
                .Include(c => c.response).ThenInclude(r => r.choices)
                .Include(c => c.response).ThenInclude(r => r.usage)
                .ToListAsync();
        }

        public async Task<ChatHistory> Insert(ChatHistory chat)
        {
            _dbContext.Chat.Add(chat);
            await _dbContext.SaveChangesAsync();
            return chat;
        }

        public async Task<ChatHistory> Update(ChatHistory chat)
        {
            _dbContext.Chat.Update(chat);
            await _dbContext.SaveChangesAsync();
            return chat;
        }

        public async Task<bool> Delete(int id)
        {
            var chat = await _dbContext.Chat.FindAsync(id);
            if (chat == null) return false;

            _dbContext.Chat.Remove(chat);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}