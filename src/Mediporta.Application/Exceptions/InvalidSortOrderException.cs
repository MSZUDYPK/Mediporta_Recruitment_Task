using Mediporta.Core.Shared;

namespace Mediporta.Application.Exceptions;

public class InvalidSortOrderException(string sortOrder) : CustomException("Invalid sort order. Allowed values are 'asc' or 'desc'.")
{
    public string SortOrder { get; } = sortOrder;
}