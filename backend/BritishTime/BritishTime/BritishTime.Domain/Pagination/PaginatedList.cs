using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace BritishTime.Domain.Pagination;
public class PaginatedList<T>
{
    public List<T> Items { get; private set; }
    public int Page { get; private set; } = 0;
    public int PageSize { get; private set; } = 10;
    public int TotalPages { get; private set; }
    public int Count { get; private set; }

    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        Page = pageIndex;
        Count = count;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        Items = items;
    }

    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;

    public static async Task<PaginatedList<T>> CreateAsync(
        IQueryable<T> source,
        int pageIndex,
        int pageSize,
        List<string> sortByMultiName,
        List<int> sortByMultiOrder
    )
    {
        var count = await source.CountAsync();

        source.MultiSort(sortByMultiName, sortByMultiOrder);

        var items = await source
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }
}
