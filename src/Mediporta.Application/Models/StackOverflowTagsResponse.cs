namespace Mediporta.Application.Models;

public class StackOverflowTagsResponse
{
    public bool HasMore { get; set; }
    public int QuotaMax { get; set; }
    public int QuotaRemaining { get; set; }
    public List<StackOverflowTag> Items { get; set; }
}