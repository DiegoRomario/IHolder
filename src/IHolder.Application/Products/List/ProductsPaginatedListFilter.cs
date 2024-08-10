using IHolder.Domain.Enumerators;
using IHolder.SharedKernel.DTO;

namespace IHolder.Application.Products.List;

public record ProductsPaginatedListFilter(
    Guid? Id,
    string? Name,
    string? Description,
    Guid? CategoryId,
    string? CategoryDescription,
    Risk? Risk,
    int PageNumber,
    short PageSize) : PaginatedFilter(PageNumber, PageSize);
