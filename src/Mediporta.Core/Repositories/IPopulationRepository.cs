using Mediporta.Core.Tags;

namespace Mediporta.Core.Repositories;

public interface IPopulationRepository
{ 
    Task<IReadOnlyList<TagName>> GetLastPopulationTagNamesAsync(CancellationToken cancellationToken = default);
    Task AddPopulationAsync(IEnumerable<Tag> tags, CancellationToken cancellationToken);
}