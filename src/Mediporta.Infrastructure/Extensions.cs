using Mediporta.Infrastructure.Exceptions;
using Mediporta.Infrastructure.Postgres;
using Mediporta.Infrastructure.Postgres.Context;
using Mediporta.Infrastructure.Postgres.Handlers;
using Mediporta.Infrastructure.StackExchange;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Mediporta.Infrastructure;

public static class Extensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Mediporta API",
                Description = "An ASP.NET Core Web API for extracting tags from StackOverflow.",
            });
        });
        services.AddExceptionHandlers();
        services.AddPostgres<StackExchangeDbContext>(configuration);
        services.AddQueryHandlers();
        services.AddCustomLogging();
        services.AddStackExchangeService(configuration);
        services.AddTagExtractor();
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Mediporta API v1");
            });
        }
        
        app.UseHttpsRedirection();
        app.UseExceptionHandlers();
        return app;
    }
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetRequiredSection(sectionName);
        section.Bind(options);

        return options;
    }
}