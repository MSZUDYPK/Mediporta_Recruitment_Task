using Mediporta.Core.Shared;

namespace Mediporta.Application.Exceptions;

public sealed class InvalidSortColumException(string sortColumn) : CustomException("Invalid sort column. Allowed values are 'name' or 'share.")
{
    public string SortColumn { get; } = sortColumn;
}