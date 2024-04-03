using Mediporta.Application.Models;

namespace Mediporta.Application.Services;

public interface ITagRetrievalStrategy
{
    public Task<IReadOnlyList<StackOverflowTag>> RetrieveTagsAsync(HttpClient httpClient, CancellationToken cancellationToken = default);
}

