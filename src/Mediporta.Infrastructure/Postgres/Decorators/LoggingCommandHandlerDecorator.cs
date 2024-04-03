using Mediporta.Application.Shared;
using Microsoft.Extensions.Logging;

namespace Mediporta.Infrastructure.Postgres.Decorators;

internal sealed class LoggingCommandHandlerDecorator<TCommand>(
    ICommandHandler<TCommand> commandHandler,
    ILogger<LoggingCommandHandlerDecorator<TCommand>> logger)
    : ICommandHandler<TCommand>
    where TCommand : class, ICommand
{
    public async Task HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        var commandName = typeof(TCommand).Name;
        logger.LogInformation("Started handling a command: {CommandName}...", commandName);
        await commandHandler.HandleAsync(command, cancellationToken);
        logger.LogInformation("Completed handling a command: {CommandName}", commandName);
    }
}