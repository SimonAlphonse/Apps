using Domain.Entities;

using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class ChatHistoryConfiguration : IEntityTypeConfiguration<ChatHistory>
    {
        public void Configure(EntityTypeBuilder<ChatHistory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StatusCode).HasConversion(
                httpStatusCode => httpStatusCode,
                value => Enum.Parse<HttpStatusCode>(value.ToString()));

            builder.HasOne(x => x.Request).WithOne();
            builder.HasOne(x => x.Response).WithOne();
        }
    }
}