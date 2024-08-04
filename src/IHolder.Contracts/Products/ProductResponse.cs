namespace IHolder.Contracts.Products;

public record ProductResponse(
    Guid Id,
    string Name,
    string Description,
    Guid CategoryId,
    string CategoryDescription,
    byte Risk,
    DateTime CreatedAt,
    DateTime? UpdatedAt = null)
{ }
