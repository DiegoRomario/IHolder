namespace IHolder.Contracts.Products;

public record ProductUpdateRequest(string Description, string Details, Guid CategoryId, byte Risk) { }
