namespace Mediporta.Core.Tags;

public sealed record Count
{
    public int Value { get; }

    public Count(int value)
    {
        if (value < 0)
        {
            throw new InvalidCountException(value);
        }

        Value = value;
    }

    public static implicit operator Count(int value) => new(value);

    public static implicit operator int(Count value) => value?.Value ?? 0;

    public override string ToString() => Value.ToString();
}