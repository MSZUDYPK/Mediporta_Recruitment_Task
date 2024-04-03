namespace Mediporta.Infrastructure.StackExchange;

public class DataFetchingOptions
{
    public const string ConfigurationSection = "DataFetching";
    
    public bool OnApplicationStart { get; init; }
}