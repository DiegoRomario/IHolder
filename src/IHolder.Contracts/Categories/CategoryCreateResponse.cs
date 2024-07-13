namespace IHolder.Contracts.Categories;

public record CategoryCreateResponse(Guid Id, string Description, string Details) { }

public record CategoryUpdateResponse(Guid Id, string Description, string Details) { }