using Mediporta.Core.Population;
using Mediporta.Core.Repositories;
using Mediporta.Core.Tags;
using Mediporta.Infrastructure.Postgres.Context;
using Microsoft.EntityFrameworkCore;

namespace Mediporta.Infrastructure.Postgres.Repositories;

public class PostgresPopulationRepository(StackExchangeDbContext stackExchangeDbContext) : IPopulationRepository
{
    private readonly DbSet<Population> _populations = stackExchangeDbContext.Populations;
    
    public async Task<IReadOnlyList<TagName>> GetLastPopulationTagNamesAsync(CancellationToken cancellationToken = default)
    {
        var lastPopulation = await _populations
            .Include(population => population.SimplifiedTags)
            .OrderByDescending(population => population.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);

        if (lastPopulation == null)
            return new List<TagName>();

        var tagNames = lastPopulation.SimplifiedTags
            .Select(t => t.Name)
            .ToList();

        return tagNames;
    }

    public async Task AddPopulationAsync(IEnumerable<Tag> tags, CancellationToken cancellationToken)
        => await _populations.AddAsync(new Population(tags), cancellationToken);
    
}