using Mediporta.Application.Exceptions;
using Mediporta.Application.Models;
using Mediporta.Application.Queries;
using Mediporta.Application.Shared;
using Mediporta.Core.Population;
using Mediporta.Core.Tags;
using Mediporta.Infrastructure.Postgres.Context;

namespace Mediporta.Infrastructure.Postgres.Handlers;

internal sealed class BrowseLastPopulationQueryHandler(StackExchangeDbContext dbContext) : IQueryHandler<BrowseLastPopulationQuery, PagedList<SimplifiedTagDto>>
{
    public async Task<PagedList<SimplifiedTagDto>> HandleAsync(BrowseLastPopulationQuery query, CancellationToken cancellationToken = default)
    { 
        IQueryable<SimplifiedTag> simplifiedTagsQuery = dbContext.SimplifiedTags;

        if (query.PageSize < 1)
            throw new InvalidPageSizeException(query.PageSize);

        if (query.Page < 1)
            throw new InvalidPageException(query.Page);
        
        if (!string.IsNullOrEmpty(query.SortColumn) && !string.IsNullOrEmpty(query.SortOrder))
        {
            simplifiedTagsQuery = query.SortColumn switch
            {
                "name" => query.SortOrder switch
                {
                    "asc" => simplifiedTagsQuery.OrderBy(tag => tag.Name),
                    "desc" => simplifiedTagsQuery.OrderByDescending(tag => tag.Name),
                    _ => throw new InvalidSortOrderException(query.SortOrder)
                },
                "share" => query.SortOrder switch
                {
                    "asc" => simplifiedTagsQuery.OrderBy(tag => tag.Share),
                    "desc" => simplifiedTagsQuery.OrderByDescending(tag => tag.Share),
                    _ => throw new InvalidSortOrderException(query.SortOrder)
                },
                _ => throw new InvalidSortColumException(query.SortColumn)
            };
        }
        
        var lastPopulationId = dbContext.Populations
            .OrderByDescending(population => population.CreatedAt)
            .Select(population => population.Id)
            .FirstOrDefault();
        
        simplifiedTagsQuery = simplifiedTagsQuery.Where(tag => tag.PopulationId == lastPopulationId);
        
        var simplifiedTagResponseQuery = simplifiedTagsQuery
            .Select(t => new SimplifiedTagDto(
                t.Name,
                t.Share));
        
        var response = await PagedList<SimplifiedTagDto>
            .CreateAsync(simplifiedTagResponseQuery, query.Page, query.PageSize, cancellationToken);
       
        return response;
    }
}


