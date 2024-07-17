namespace IHolder.Contracts.Categories;

public record CategoryResponse(Guid Id, string Description, string Details, DateTime CreatedAt, DateTime? UpdatedAt = null) { }
