using Mediporta.Application.Shared;
using Mediporta.Core.Repositories;
using Mediporta.Infrastructure.Postgres.Context;
using Mediporta.Infrastructure.Postgres.Decorators;
using Mediporta.Infrastructure.Postgres.Repositories;
using Mediporta.Infrastructure.StackExchange;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Mediporta.Infrastructure.Postgres;

internal static class Extensions
{
    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPopulationRepository, PostgresPopulationRepository>();
    }
    public static IServiceCollection AddPostgres<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
    {
        services.Configure<PostgresOptions>(configuration.GetRequiredSection(PostgresOptions.ConfigurationSection));
        var postgresOptions = configuration.GetOptions<PostgresOptions>(PostgresOptions.ConfigurationSection);
        var connectionString = postgresOptions.ConnectionString;
        services.AddDbContext<T>(x => x.UseNpgsql(connectionString));
        services.AddRepositories();
        services.AddHostedService<DatabaseInitializer<StackExchangeDbContext>>();
        services.Configure<DataFetchingOptions>(configuration.GetRequiredSection(DataFetchingOptions.ConfigurationSection));
        services.AddScoped<IUnitOfWork, PostgresUnitOfWork<StackExchangeDbContext>>();
        services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
        
        return services;
    }
    public static void AddCustomLogging(this IServiceCollection services)
    {
        services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
    }
}