using Mediporta.Application.Models;
using Mediporta.Application.Shared;
using Mediporta.Core.Tags;
using Microsoft.Extensions.DependencyInjection;

namespace Mediporta.Infrastructure.Postgres.Handlers;

internal static class Extensions
{
    public static IServiceCollection AddQueryHandlers(this IServiceCollection services)
    {
        var infrastructureAssembly = typeof(BrowseLastPopulationQueryHandler).Assembly;
        
        services.Scan(s => s.FromAssemblies(infrastructureAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
    
    public static SimplifiedTagDto AsDto(this SimplifiedTag entity) => new SimplifiedTagDto(entity.Name, entity.Share);
}