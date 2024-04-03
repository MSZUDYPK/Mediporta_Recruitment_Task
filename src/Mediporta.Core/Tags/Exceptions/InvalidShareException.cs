using Mediporta.Core.Shared;

namespace Mediporta.Core.Tags;

public sealed class InvalidShareException(double share) : CustomException($"Cannot set: {share} as share. Share have to be between 0 and 100.")
{
    public double Share { get; } = share;
}