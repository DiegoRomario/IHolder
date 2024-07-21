namespace IHolder.Contracts.Products;

public record ProductCreateRequest(string Description, string Details, Guid CategoryId, byte Risk) { }
