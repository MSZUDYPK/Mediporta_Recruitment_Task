using Mediporta.Api.Endpoints;
using Mediporta.Application;
using Mediporta.Application.Commands;
using Mediporta.Application.Shared;
using Mediporta.Core;
using Mediporta.Infrastructure;
using Mediporta.Infrastructure.StackExchange;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseInfrastructure();
app.UsePopulationEndpoints();

var dataFetchingOptions = builder.Configuration.GetOptions<DataFetchingOptions>(DataFetchingOptions.ConfigurationSection);
if (dataFetchingOptions.OnApplicationStart)
{
    app.Lifetime.ApplicationStarted.Register(() =>
    {
        var task = OnApplicationStarted(app.Services);
        task.ContinueWith(t => { }, TaskScheduler.Default).Wait(app.Lifetime.ApplicationStopping);
    });
}

app.Run();
return;

static async Task OnApplicationStarted(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var pullTagsCommandHandler = scope.ServiceProvider.GetRequiredService<ICommandHandler<PullTagsCommand>>();
    var command = new PullTagsCommand(null);
    await pullTagsCommandHandler.HandleAsync(command);
}

public partial class Program { }

