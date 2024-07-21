namespace IHolder.Contracts.Products;

public record ProductResponse(
    Guid Id,
    string Description,
    string Details,
    Guid CategoryId,
    string CategoryDescription,
    byte Risk,
    DateTime CreatedAt,
    DateTime? UpdatedAt = null)
{ }
