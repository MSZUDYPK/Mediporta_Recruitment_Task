using Mediporta.Core.Shared;

namespace Mediporta.Application.Exceptions;

public sealed class InvalidPageException(int page) : CustomException("Page number have to be at least 1.")
{
    public int Page { get; } = page;
}