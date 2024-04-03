using Mediporta.Core.Population;

namespace Mediporta.Core.Tags;

public sealed record SimplifiedTag(TagId Id, TagName Name, Share Share, PopulationId PopulationId);