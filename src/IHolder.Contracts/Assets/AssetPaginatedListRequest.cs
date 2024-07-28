using IHolder.SharedKernel.DTO;

namespace IHolder.Contracts.Assets;

public record AssetPaginatedListRequest : PaginatedFilter
{
    public Guid? Id { get; set; }
    public string? Description { get; set; }
    public string? Details { get; set; }
    public string? Ticker { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public Guid? ProductId { get; set; }
    public string? ProductDescription { get; set; }
    public Guid? CategoryId { get; set; }
    public string? CategoryDescription { get; set; }
}
