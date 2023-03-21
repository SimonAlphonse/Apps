using ChatCompletion.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatCompletion.Api.DbContexts
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {

        }

        public DbSet<ChatHistory> Chat { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Choice> Choice { get; set; }
        public DbSet<Usage> Usage { get; set; }
        public DbSet<Request> ChatCompletionRequest { get; set; }
        public DbSet<Response> ChatCompletionResponse { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "MyChatGpt");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1 <-> 1 relationship

            modelBuilder.Entity<ChatHistory>()
               .HasOne(o => o.request)
               .WithOne()
               .HasForeignKey<Request>(fk => fk.id)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChatHistory>()
                .HasOne(o => o.response)
                .WithOne()
                .HasForeignKey<Response>(fk => fk.id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Response>()
                .HasOne(o => o.usage)
                .WithOne()
                .HasForeignKey<Usage>(fk => fk.id)
                .OnDelete(DeleteBehavior.Cascade);

            // 1 <-> Many relationship

            modelBuilder.Entity<Request>()
                .HasMany(m => m.messages)
                .WithOne()
                .HasForeignKey(fk => fk.id);

            modelBuilder.Entity<Response>()
               .HasMany(m => m.choices)
               .WithOne(o=> o.response);
        }
    }
}