using Mediporta.Core.Tags;

namespace Mediporta.Application.Services;

public interface ITagExtractor
{
    public Task<IReadOnlyList<Tag>> ExtractTags(string[]? commandMissingTags, CancellationToken cancellationToken = default);
}