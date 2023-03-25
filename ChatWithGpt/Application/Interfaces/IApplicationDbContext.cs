using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ChatHistory> ChatHistory { get; set; }
        DbSet<Request> ChatRequest { get; set; }
        DbSet<Response> ChatResponse { get; set; }
        DbSet<Message> Message { get; set; }
        DbSet<Choice> Choice { get; set; }
        DbSet<Usage> Usage { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}