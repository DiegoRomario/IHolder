using IHolder.SharedKernel.DTO;

namespace IHolder.Contracts.Products;

public record ProductPaginatedListRequest : PaginatedFilter
{
    public Guid? Id { get; set; }
    public string? Description { get; set; }
    public string? Details { get; set; }
    public byte? Risk { get; set; }
    public Guid? CategoryId { get; set; }
    public string? CategoryDescription { get; set; }
}

