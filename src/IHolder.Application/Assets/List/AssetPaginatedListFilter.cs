using IHolder.SharedKernel.DTO;

namespace IHolder.Application.Assets.List;

public record AssetPaginatedListFilter(
    Guid? Id,
    string? Description,
    string? Details,
    string? Ticker,
    decimal? MinPrice,
    decimal? MaxPrice,
    Guid? ProductId,
    string? ProductDescription,
    Guid? CategoryId,
    string? CategoryDescription,
    int PageNumber,
    short PageSize) : PaginatedFilter(PageNumber, PageSize);
