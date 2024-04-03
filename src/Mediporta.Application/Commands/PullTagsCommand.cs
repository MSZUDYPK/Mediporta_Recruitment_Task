using Mediporta.Application.Shared;

namespace Mediporta.Application.Commands;

public record PullTagsCommand(string[]? MissingTags) : ICommand;
