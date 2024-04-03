using Mediporta.Application.Services;
using Mediporta.Application.Shared;
using Mediporta.Core.Repositories;

namespace Mediporta.Application.Commands.Handlers;

public class PullTagsCommandHandler(
    ITagExtractor tagExtractor,
    IPopulationRepository populationRepository
    ) : ICommandHandler<PullTagsCommand>
{
    public async Task HandleAsync(PullTagsCommand command, CancellationToken cancellationToken = default)
    {
        var tags = await tagExtractor.ExtractTags(command.MissingTags, cancellationToken);
        await populationRepository.AddPopulationAsync(tags, cancellationToken);
    }
}
