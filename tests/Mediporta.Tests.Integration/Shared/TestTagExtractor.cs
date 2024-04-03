using Mediporta.Application.Services;
using Mediporta.Core.Repositories;
using Mediporta.Core.Tags;

namespace Mediporta.Tests.Integration.Shared;

internal class TestTagExtractor(IStackExchangeService stackExchangeService, IPopulationRepository populationRepository) : ITagExtractor
{
    public async Task<IReadOnlyList<Tag>> ExtractTags(string[]? commandMissingTags, CancellationToken cancellationToken = default)
    {
        var missingTagsNames = commandMissingTags != null ? new HashSet<string>(commandMissingTags) : new HashSet<string>();

        if (missingTagsNames.Count == 0)
        {
            stackExchangeService.SetStackOverflowTagsRetrievalStrategy(new InMemoryRetrieveAllTags());
        }
        else
        {
            var existingTagsNames = await populationRepository.GetLastPopulationTagNamesAsync(cancellationToken);
            
            foreach (var tagName in existingTagsNames)
            {
                missingTagsNames.Add(tagName.Value);
            }
            
            stackExchangeService.SetStackOverflowTagsRetrievalStrategy(new InMemoryRetrieveMissingTagsTags(missingTagsNames));
        }
        
        var stackOverflowTags = await stackExchangeService.GetStackOverflowTagsAsync(cancellationToken);
        
        return stackOverflowTags.Select(tag => new Tag(tag.Name, tag.Count)).ToList();
    }
}