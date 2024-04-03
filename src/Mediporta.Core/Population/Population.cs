using Mediporta.Core.Population.Exceptions;
using Mediporta.Core.Tags;

namespace Mediporta.Core.Population;

public class Population
{
    private List<SimplifiedTag> _simplifiedTags;
    public PopulationId Id { get; private set; }
    public IReadOnlyList<SimplifiedTag> SimplifiedTags => _simplifiedTags;
    
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    
    private Population()
    {
    }
    
    public Population(IEnumerable<Tag> tags)
    {
        var tagCount = tags.Count();
        if (tagCount < 1000)
        {
            throw new InvalidPopulationSizeException(tagCount);
        }
        
        Id = new PopulationId(Guid.NewGuid());
        _simplifiedTags = CalculateTagsShares(tags, Id);
    }

    private List<SimplifiedTag> CalculateTagsShares(IEnumerable<Tag> tags, PopulationId populationId)
    {
        var totalShare = tags.Sum(c => c.Count);
        
        return tags.Select(c => new SimplifiedTag(
            new TagId(Guid.NewGuid()), 
            c.Name, 
            new Share((double)c.Count / totalShare),
            populationId))
            .ToList();
    }
}


