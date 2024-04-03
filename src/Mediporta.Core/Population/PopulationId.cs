namespace Mediporta.Core.Population;

public sealed record PopulationId(Guid Value)
{
    public static implicit operator Guid(PopulationId id) => id.Value;
    public static implicit operator PopulationId(Guid id) => id.Equals(Guid.Empty) ? null : new PopulationId(id);
}