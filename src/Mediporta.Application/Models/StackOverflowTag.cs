namespace Mediporta.Application.Models;

public class StackOverflowTag
{
    public int Count { get; set; }
    public bool HasSynonyms { get; set; }
    public bool IsModeratorOnly { get; set; }
    public bool IsRequired { get; set; }
    public string Name { get; set; }
    public string[] Synonyms { get; set; }
    public List<Collectives>? Collectives { get; set; } = null;
    public DateTimeOffset? LastActivityDate { get; set; } = null;
    public int? UserId { get; set; } = null;
}
public class Collectives
{
    public string Description { get; set; }
    public List<CollectiveExternalLinks> ExternalLinks { get; set; }
    public string Link { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string[] Tags { get; set; }
}

public class CollectiveExternalLinks
{
    public string Link { get; set; }
    public string Type { get; set; }
}