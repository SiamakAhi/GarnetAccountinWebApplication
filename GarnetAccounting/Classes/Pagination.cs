
public class Pagination<T>
{
    public IQueryable<T> Items { get; set; }
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);

    public Pagination(IQueryable<T> items, int totalItems, int currentPage, int pageSize)
    {
        Items = items;
        TotalItems = totalItems;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }

    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;

    public static Pagination<T> Create(IQueryable<T> source, int currentPage, int pageSize)
    {
        var totalItems = source.Count();
        var items = source.Skip((currentPage - 1) * pageSize).Take(pageSize);

        return new Pagination<T>(items, totalItems, currentPage, pageSize);
    }
}

