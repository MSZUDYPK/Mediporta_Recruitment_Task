using Mediporta.Application.Models;
using Mediporta.Application.Services;
using Mediporta.Infrastructure.StackExchange.TagRetrieval;

namespace Mediporta.Infrastructure.StackExchange.Services;

public sealed class StackExchangeHttpClient(HttpClient httpClient) : IStackExchangeService
{
    private ITagRetrievalStrategy _tagRetrievalStrategy = new RetrieveAllTags();

    public Task<IReadOnlyList<StackOverflowTag>> GetStackOverflowTagsAsync(CancellationToken cancellationToken = default)
        => _tagRetrievalStrategy.RetrieveTagsAsync(httpClient, cancellationToken);

    public void SetStackOverflowTagsRetrievalStrategy(ITagRetrievalStrategy tagRetrievalStrategy)
        => _tagRetrievalStrategy = tagRetrievalStrategy;
}