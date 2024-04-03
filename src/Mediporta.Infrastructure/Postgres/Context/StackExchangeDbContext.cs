using Mediporta.Core.Population;
using Mediporta.Core.Tags;
using Microsoft.EntityFrameworkCore;

namespace Mediporta.Infrastructure.Postgres.Context;

public class StackExchangeDbContext(DbContextOptions<StackExchangeDbContext> options) : DbContext(options)
{
    public DbSet<Population> Populations { get; set; }
    public DbSet<SimplifiedTag> SimplifiedTags { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}