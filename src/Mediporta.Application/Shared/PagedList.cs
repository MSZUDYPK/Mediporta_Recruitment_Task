using Microsoft.EntityFrameworkCore;

namespace Mediporta.Application.Shared;

public class PagedList<T>
{
    private List<T> _items { get;  } 
    public int Page { get; }
    public int PageSize { get;  }
    public int TotalCount { get;  }
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;
    public IReadOnlyList<T> Items => _items.AsReadOnly();
    
    private PagedList(List<T> items, int page, int pageSize, int totalCount)
    {
        _items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
    
    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var count = await source.CountAsync(cancellationToken); ;
        
        var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return new(items, page, pageSize, count);
    }
}