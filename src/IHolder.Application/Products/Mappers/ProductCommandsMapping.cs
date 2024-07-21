using IHolder.Application.Products.Create;
using IHolder.Application.Products.Update;
using IHolder.Domain.Products;

namespace IHolder.Application.Products.Mappers;

public static class ProductCommandsMapping
{
    public static Product ToProductEntity(this ProductCreateCommand command)
    {
        return new Product(command.Description, command.Details, command.CategoryId, command.Risk);
    }

    public static Product ToProductEntity(this ProductUpdateCommand command)
    {
        return new Product(command.Description, command.Details, command.CategoryId, command.Risk, id: command.Id);
    }
}
