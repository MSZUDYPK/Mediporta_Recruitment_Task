namespace Mediporta.Core.Tags;

public sealed record TagId(Guid Value)
{
    public static implicit operator Guid(TagId id) => id.Value;
    public static implicit operator TagId(Guid id) => id.Equals(Guid.Empty) ? null : new TagId(id);
}