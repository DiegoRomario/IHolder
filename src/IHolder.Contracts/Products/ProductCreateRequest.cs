namespace IHolder.Contracts.Products;

public record ProductCreateRequest(string Name, string Description, Guid CategoryId, byte Risk) { }
