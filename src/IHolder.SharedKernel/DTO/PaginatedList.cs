namespace IHolder.SharedKernel.DTO;

public record PaginatedList<T>(IReadOnlyList<T> Items, int TotalCount, int PageNumber, short PageSize)
{
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}
