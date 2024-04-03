namespace Mediporta.Application.Shared;

//Marker
public interface ICommand
{
}
//Marker
public interface IQuery
{
    
}

public interface IQuery<T> : IQuery
{
}

public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
{
    public Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    public Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}

