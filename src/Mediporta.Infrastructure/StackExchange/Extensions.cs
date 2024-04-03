using System.Net;
using Mediporta.Application.Services;
using Mediporta.Infrastructure.StackExchange.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mediporta.Infrastructure.StackExchange;

internal static class Extensions
{

    private const string OptionsSectionName = "StackExchangeAPI";
    public static IServiceCollection AddStackExchangeService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<StackExchangeOptions>(configuration.GetRequiredSection(StackExchangeOptions.ConfigurationSection));
        var stackExchangeOptions = configuration.GetOptions<StackExchangeOptions>(StackExchangeOptions.ConfigurationSection);
        
        services.AddHttpClient<IStackExchangeService, StackExchangeHttpClient>(client =>
            {
                client.BaseAddress = new Uri(stackExchangeOptions.BaseUrl);
            })
            .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                PooledConnectionLifetime = TimeSpan.FromMinutes(5)
            })
            .SetHandlerLifetime(Timeout.InfiniteTimeSpan);

        return services;
    }
    
    public static IServiceCollection AddTagExtractor(this IServiceCollection services)
    {
        services.AddScoped<ITagExtractor, TagExtractor>();
        return services;
    }
}