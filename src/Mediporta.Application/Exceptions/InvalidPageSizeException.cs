using Mediporta.Core.Shared;

namespace Mediporta.Application.Exceptions;

public sealed class InvalidPageSizeException(int pageSize) : CustomException("Page size have to be at least 1")
{
    public int PageSize { get; } = pageSize;
}