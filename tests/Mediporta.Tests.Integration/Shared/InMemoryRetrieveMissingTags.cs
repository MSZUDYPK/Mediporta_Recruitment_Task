using Mediporta.Application.Models;
using Mediporta.Application.Services;

namespace Mediporta.Tests.Integration.Shared;

internal class InMemoryRetrieveMissingTagsTags(IEnumerable<string> missingTagsName) : ITagRetrievalStrategy
{
    public async Task<IReadOnlyList<StackOverflowTag>> RetrieveTagsAsync(HttpClient httpClient, CancellationToken cancellationToken = default)
    {
        var tags = missingTagsName.Select(missingTagName => new StackOverflowTag { Name = missingTagName, Count = 1 }).ToList();

        await Task.CompletedTask;
        
        return tags;
    }
}