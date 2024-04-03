using Mediporta.Core.Population;
using Mediporta.Core.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mediporta.Infrastructure.Postgres.Configurations;

internal sealed class SimplifiedTagConfiguration : IEntityTypeConfiguration<SimplifiedTag>
{
    public void Configure(EntityTypeBuilder<SimplifiedTag> builder)
    {
        builder.HasKey(st => st.Id);
        builder.Property(st => st.Id)
            .HasConversion(st => st.Value, x => new TagId(x))
            .IsRequired();

        builder.Property(st => st.Name)
            .HasConversion(st => st.Value, x => new TagName(x))
            .IsRequired();

        builder.Property(st => st.Share)
            .HasConversion(st => st.Value, x => new Share(x))
            .IsRequired();
    }
}