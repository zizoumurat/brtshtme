namespace BritishTime.Domain.Pagination;

public record PageRequest
{
    private int _page = 0;
    public int Page
    {
        get { return _page + 1; }
        set { _page = value; }
    }
    public int PageSize { get; set; } = 10;
    public List<string> sortByMultiName { get; set; }
    public List<int> sortByMultiOrder { get; set; }
}
