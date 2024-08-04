using IHolder.SharedKernel.DTO;

namespace IHolder.Application.Categories.List;

public record CategoryPaginatedListFilter(Guid? Id, string? Name, string? Description, int PageNumber, short PageSize) : PaginatedFilter(PageNumber, PageSize);
