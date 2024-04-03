using Mediporta.Core.Shared;

namespace Mediporta.Core.Tags;

public sealed class InvalidCountException(int count) : CustomException($"Cannot set: {count} as Count. Count have to be at least 0.")
{
    public int Count { get; } = count;
}


