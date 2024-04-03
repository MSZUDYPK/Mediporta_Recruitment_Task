using Microsoft.EntityFrameworkCore;

namespace Mediporta.Infrastructure.Postgres.Decorators;
public interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action);
}

internal sealed class PostgresUnitOfWork<T>(T dbContext) : IUnitOfWork
    where T : DbContext
{
    public async Task ExecuteAsync(Func<Task> action)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        try
        {
            await action();
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}