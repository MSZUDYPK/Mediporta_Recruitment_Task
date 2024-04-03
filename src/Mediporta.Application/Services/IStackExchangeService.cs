using Mediporta.Application.Models;

namespace Mediporta.Application.Services;

public interface IStackExchangeService
{
    public Task<IReadOnlyList<StackOverflowTag>> GetStackOverflowTagsAsync(CancellationToken cancellationToken = default);
    public void SetStackOverflowTagsRetrievalStrategy(ITagRetrievalStrategy tagRetrievalStrategy);
}