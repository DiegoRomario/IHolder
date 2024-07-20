namespace IHolder.SharedKernel.DTO;

public record PaginatedFilter
{
    private const short MinPageSize = 0;
    private const short MinPageNumber = 1;
    private const short DefaultPageSize = 10;

    public int PageNumber { get; init; }
    public short PageSize { get; init; }

    public PaginatedFilter(int pageNumber = MinPageNumber, short pageSize = DefaultPageSize)
    {
        PageNumber = EnsurePageNumberInRange(pageNumber);
        PageSize = EnsurePageSizeInRange(pageSize);
    }

    private static int EnsurePageNumberInRange(int pageNumber) => pageNumber <= 0 ? MinPageNumber : pageNumber;

    private static short EnsurePageSizeInRange(short pageSize) => pageSize < MinPageSize ? MinPageSize : pageSize;
}

