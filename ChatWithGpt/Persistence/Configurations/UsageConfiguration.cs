using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class UsageConfiguration : IEntityTypeConfiguration<Usage>
    {
        public void Configure(EntityTypeBuilder<Usage> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}