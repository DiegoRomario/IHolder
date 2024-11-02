namespace IHolder.SharedKernel.DTO;

public record PaginatedFilter
{
    private const short MinPageSize = 1;
    private const short MinPageNumber = 1;
    private const short DefaultPageSize = 10;

    public int PageNumber { get; init; }
    public short PageSize { get; init; }

    public PaginatedFilter(int pageNumber = MinPageNumber, short pageSize = DefaultPageSize)
    {
        PageNumber = ValidatePageNumber(pageNumber);
        PageSize = ValidatePageSize(pageSize);
    }

    private static int ValidatePageNumber(int pageNumber) => pageNumber < MinPageNumber ? MinPageNumber : pageNumber;

    private static short ValidatePageSize(short pageSize) => pageSize < MinPageSize ? DefaultPageSize : pageSize;
}
