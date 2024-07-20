using IHolder.SharedKernel.DTO;

namespace IHolder.Contracts.Categories;

public record CategoryPaginatedListRequest : PaginatedFilter
{
    public Guid? Id { get; set; }
    public string? Description { get; set; }
    public string? Details { get; set; }
}

