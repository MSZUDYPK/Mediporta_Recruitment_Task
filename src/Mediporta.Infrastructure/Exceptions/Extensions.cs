using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Mediporta.Infrastructure.Exceptions;

internal static class Extensions
{
    public static void AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<DefaultExceptionHandler>();
    }
    
    public static void UseExceptionHandlers(this WebApplication app)
    {
        app.UseExceptionHandler(opt => { });
    }
}