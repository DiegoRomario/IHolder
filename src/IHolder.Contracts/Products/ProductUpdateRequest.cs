namespace IHolder.Contracts.Products;

public record ProductUpdateRequest(string Name, string Description, Guid CategoryId, byte Risk) { }
