using Mediporta.Core.Population;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mediporta.Infrastructure.Postgres.Configurations;

internal sealed class PopulationConfiguration : IEntityTypeConfiguration<Population>
{
    public void Configure(EntityTypeBuilder<Population> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .HasConversion(p => p.Value, x => new PopulationId(x))
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.HasMany(p => p.SimplifiedTags)
            .WithOne()
            .HasForeignKey(st => st.PopulationId); 
        }
}
