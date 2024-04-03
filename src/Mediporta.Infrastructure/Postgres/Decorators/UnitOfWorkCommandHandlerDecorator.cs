using Mediporta.Application.Shared;

namespace Mediporta.Infrastructure.Postgres.Decorators;

internal sealed class UnitOfWorkCommandHandlerDecorator<TCommand>(
    ICommandHandler<TCommand> commandHandler,
    IUnitOfWork unitOfWork)
    : ICommandHandler<TCommand>
    where TCommand : ICommand
{
    public async Task HandleAsync(TCommand command, CancellationToken cancellationToken = default)
    {
        await unitOfWork.ExecuteAsync(() => commandHandler.HandleAsync(command, cancellationToken));
    }
}