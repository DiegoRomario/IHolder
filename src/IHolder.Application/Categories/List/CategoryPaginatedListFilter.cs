using IHolder.SharedKernel.DTO;

namespace IHolder.Application.Categories.List;

public record CategoryPaginatedListFilter(Guid? Id, string? Description, string? Details, int PageNumber, short PageSize) : PaginatedFilter(PageNumber, PageSize);
