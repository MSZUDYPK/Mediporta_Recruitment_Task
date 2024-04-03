using Mediporta.Application.Models;
using Mediporta.Application.Services;

namespace Mediporta.Tests.Integration.Shared;

internal class InMemoryRetrieveAllTags : ITagRetrievalStrategy
{
    public async Task<IReadOnlyList<StackOverflowTag>> RetrieveTagsAsync(HttpClient httpClient, CancellationToken cancellationToken = default)
    {
        var tags = new List<StackOverflowTag>();
        
        for (var i = 0; i < 1000; i++)
        {
            tags.Add(new StackOverflowTag { Name = $"Tag{i}", Count = i });
        }
        
        await Task.CompletedTask;
        
        return tags;
    }
}