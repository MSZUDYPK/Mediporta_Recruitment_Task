using Mediporta.Infrastructure.Postgres.Context;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace Mediporta.Tests.Integration.Shared;

public abstract class ApiTests : IAsyncLifetime
{
    private TestApi _testApi = null!;
    
    protected HttpClient Client => _testApi.Client;
    
    protected StackExchangeDbContext TestDbContext { get; private set; } = null!;
    
    protected PostgreSqlContainer PostgreSqlContainer { get; private set; } = null!;
    
    protected virtual Action<IServiceCollection>? ConfigureServices { get; } = null;
    
    public virtual async Task InitializeAsync()
    {
        PostgreSqlContainer = await TestDatabase.InitPostgresContainerAsync();
        var connectionString = PostgreSqlContainer.GetConnectionString();
        TestDbContext = TestDatabase.CreateDbContext(connectionString);
        _testApi = new TestApi(ConfigureServices, new Dictionary<string, string>
        {
            { "Postgres:ConnectionString", connectionString}
        });
    }

    public virtual async Task DisposeAsync()
    {
        await _testApi.DisposeAsync();
        await PostgreSqlContainer.DisposeAsync();
    }
}