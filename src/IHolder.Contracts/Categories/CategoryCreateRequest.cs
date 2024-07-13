namespace IHolder.Contracts.Categories;

public record CategoryCreateRequest(string Description, string Details) { }

public record CategoryUpdateRequest(string Description, string Details) { }
