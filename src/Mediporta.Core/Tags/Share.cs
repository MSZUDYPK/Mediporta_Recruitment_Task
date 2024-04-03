namespace Mediporta.Core.Tags;

public sealed record Share
{
    public double Value { get; }
    
    public Share(double value)
    {
        if (value is < 0 or > 100)
        {
            throw new InvalidShareException(value);
        }

        Value = value;
    }

    public static implicit operator Share(double value) => new(value);

    public static implicit operator double(Share value) => value?.Value ?? 0;

    public override string ToString() => $"{Value:P}";
}