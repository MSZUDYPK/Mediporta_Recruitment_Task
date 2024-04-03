using Mediporta.Core.Shared;

namespace Mediporta.Core.Population.Exceptions;

public sealed class InvalidPopulationSizeException(int count) : CustomException($"Population size have to be at least 1000. Current size: {count}.")
{
    public int Count { get; } = count;
}