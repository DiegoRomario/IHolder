namespace IHolder.Contracts.Categories;

public record CategoryResponse(Guid Id, string Name, string Description, DateTime CreatedAt, DateTime? UpdatedAt = null) { }
