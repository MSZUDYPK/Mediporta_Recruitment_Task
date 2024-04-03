namespace Mediporta.Core.Tags;

public sealed record TagName
{
    public string Value { get; }

    public TagName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidTagNameException(value);
        }

        Value = value;
    }

    public static implicit operator TagName(string value) => new(value);

    public static implicit operator string(TagName value) => value?.Value ?? string.Empty;

    public override string ToString() => Value;
}