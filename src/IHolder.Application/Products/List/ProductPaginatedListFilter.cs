using IHolder.Domain.Enumerators;
using IHolder.SharedKernel.DTO;

namespace IHolder.Application.Products.List;

public record ProductPaginatedListFilter(
    Guid? Id,
    string? Description,
    string? Details,
    string? CategoryDescription,
    Risk? Risk,
    int PageNumber,
    short PageSize) : PaginatedFilter(PageNumber, PageSize);
