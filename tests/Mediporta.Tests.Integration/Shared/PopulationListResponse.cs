using Mediporta.Application.Models;

namespace Mediporta.Tests.Integration.Shared;

public record PopulationListResponse(List<SimplifiedTagDto> Items, int Page, int PageSize, int TotalCount, bool HasNextPage, bool HasPreviousPage);