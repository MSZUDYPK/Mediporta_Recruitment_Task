using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mediporta.Tests.Integration.Shared;

internal sealed class TestApi : WebApplicationFactory<Program>
{
    public HttpClient Client { get; }
    
    public TestApi(Action<IServiceCollection>? services = null, Dictionary<string, string>? options = null)
    {
        Client = WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Development");
            if (services is not null)
            {
                builder.ConfigureTestServices(services);
            }
            if (options is not null)
            {
                var configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection(options!)
                    .Build();
                builder.UseConfiguration(configuration);
            }
        }).CreateClient();
    }
}