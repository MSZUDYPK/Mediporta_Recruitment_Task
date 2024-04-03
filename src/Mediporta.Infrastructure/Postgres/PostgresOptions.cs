namespace Mediporta.Infrastructure.Postgres;

public sealed class PostgresOptions
{
    public const string ConfigurationSection = "Postgres";
    public string ConnectionString { get; init; } = string.Empty;
}