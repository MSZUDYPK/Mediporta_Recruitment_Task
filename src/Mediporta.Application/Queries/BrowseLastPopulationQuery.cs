using Mediporta.Application.Models;
using Mediporta.Application.Shared;

namespace Mediporta.Application.Queries;

public record BrowseLastPopulationQuery(int Page, int PageSize, string? SortColumn, string? SortOrder) 
    : IQuery<PagedList<SimplifiedTagDto>>;