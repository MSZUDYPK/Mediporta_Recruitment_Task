using Mediporta.Application.Services;
using Mediporta.Core.Repositories;
using Mediporta.Core.Tags;
using Mediporta.Infrastructure.StackExchange.TagRetrieval;

namespace Mediporta.Infrastructure.StackExchange.Services;

public class TagExtractor(
    IStackExchangeService stackExchangeService,
    IPopulationRepository populationRepository
    ) : ITagExtractor
{
    public async Task<IReadOnlyList<Tag>> ExtractTags(string[]? commandMissingTagsNames, CancellationToken cancellationToken = default)
    {
        var missingTagsNames = commandMissingTagsNames != null ? [..commandMissingTagsNames] : new HashSet<string>();

        if (missingTagsNames.Count == 0)
        {
            stackExchangeService.SetStackOverflowTagsRetrievalStrategy(new RetrieveAllTags());
        }
        else
        {
            // Get the last population tag names from the database
            var existingTagNames = await populationRepository.GetLastPopulationTagNamesAsync(cancellationToken);
            
            // Add the names of the existing tags to the missingTagsNames HashSet
            // If a tag name already exists in the HashSet, it will not be added again
            foreach (var tagName in existingTagNames)
            {
                missingTagsNames.Add(tagName.Value);
            }
            
            stackExchangeService.SetStackOverflowTagsRetrievalStrategy(new RetrieveMissingTags(missingTagsNames));
        }
        
        var stackOverflowTags = await stackExchangeService.GetStackOverflowTagsAsync(cancellationToken);
        
        return stackOverflowTags.Select(tag => new Tag(tag.Name, tag.Count)).ToList();
    }
}