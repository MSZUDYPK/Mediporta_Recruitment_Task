using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using FluentAssertions;
using Mediporta.Infrastructure.Postgres.Context;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace Mediporta.Tests.Integration.Shared;

internal static class TestDatabase
{
    public static StackExchangeDbContext CreateDbContext(string connectionString)
        => new(new DbContextOptionsBuilder<StackExchangeDbContext>().UseNpgsql(connectionString).Options);

    public static async Task<PostgreSqlContainer> InitPostgresContainerAsync()
    {
        const int port = 5432;
        var container = new PostgreSqlBuilder()
            .WithExposedPort(port)
            .WithPortBinding(port, true)
            .WithDatabase("stackexchange")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();
        await container.StartAsync();
        
        return container;
    }
}