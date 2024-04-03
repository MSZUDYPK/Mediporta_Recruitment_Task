using Mediporta.Core.Shared;

namespace Mediporta.Core.Tags;

public sealed class InvalidTagNameException(string tagName) : CustomException($"Cannot set: {tagName} as tagName.")
{
    public string TagName { get; } = tagName;
}