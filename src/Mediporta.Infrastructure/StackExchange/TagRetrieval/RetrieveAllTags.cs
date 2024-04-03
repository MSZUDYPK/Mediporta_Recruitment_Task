using System.Net.Http.Json;
using Mediporta.Application.Models;
using Mediporta.Application.Services;

namespace Mediporta.Infrastructure.StackExchange.TagRetrieval;

public class RetrieveAllTags : ITagRetrievalStrategy
{
    public async Task<IReadOnlyList<StackOverflowTag>> RetrieveTagsAsync(HttpClient httpClient, CancellationToken cancellationToken = default)
    {
         var tags = new List<StackOverflowTag>();

         for (var page = 1; page <= 10 ; page++)
         {
             var requestUri = $"tags?page={page}&pagesize=100&order=desc&sort=popular&site=stackoverflow";
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