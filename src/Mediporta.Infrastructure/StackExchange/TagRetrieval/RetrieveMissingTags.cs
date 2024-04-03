using System.Net.Http.Json;
using Mediporta.Application.Models;
using Mediporta.Application.Services;

namespace Mediporta.Infrastructure.StackExchange.TagRetrieval;

public class RetrieveMissingTags(IEnumerable<string> missingTagsNames) : ITagRetrievalStrategy
{
    public async Task<IReadOnlyList<StackOverflowTag>> RetrieveTagsAsync(HttpClient httpClient, CancellationToken cancellationToken = default)
    {
        var tags = new List<StackOverflowTag>();

        // Split the tag names into batches of 100
        var batches = missingTagsNames.Select((tagName, index) => new { tagName, index })
            .GroupBy(x => x.index / 100)
            .Select(g => g.Select(x => x.tagName));

        foreach (var batch in batches)
        {
            // Join the tag names in the current batch with a semicolon to create a vectorized list
            var vectorizedTagNames = string.Join(";", batch.Select(System.Net.WebUtility.UrlEncode));

            var requestUri = $"tags/{vectorizedTagNames}/info?pagesize=100&order=desc&sort=popular&site=stackoverflow";
            var response = await httpClient.GetFromJsonAsync<StackOverflowTagsResponse>(requestUri, cancellationToken);

            if (response is null)
            {
                throw new HttpRequestException("No response from StackExchange API");
            }

            tags.AddRange(response.Items);
        }

        return tags;
    }
}