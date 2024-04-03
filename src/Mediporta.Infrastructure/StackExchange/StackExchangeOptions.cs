namespace Mediporta.Infrastructure.StackExchange;

internal sealed class StackExchangeOptions
{
    public const string ConfigurationSection = "StackExchangeAPI";
    
    public string BaseUrl { get; init; } = string.Empty;
}