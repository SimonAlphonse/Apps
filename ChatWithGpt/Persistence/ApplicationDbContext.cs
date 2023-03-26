using Domain.Entities;
using Application.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ChatHistory> ChatHistory { get; set; }
        public DbSet<Request> ChatRequest { get; set; }
        public DbSet<Response> ChatResponse { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Choice> Choice { get; set; }
        public DbSet<Usage> Usage { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=chat-database.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
        }
    }
}