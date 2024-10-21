using IHolder.Application.Products.Create;
using IHolder.Application.Products.Update;
using IHolder.Domain.Products;

namespace IHolder.Application.Products.Mappers;

public static class ProductCommandsMapping
{
    public static Product ToEntity(this ProductCreateCommand command)
    {
        return new Product(command.Name, command.Description, command.CategoryId, command.Risk, command.ExchangeId);
    }

    public static Product ToEntity(this ProductUpdateCommand command)
    {
        return new Product(command.Name, command.Description, command.CategoryId, command.Risk, command.ExchangeId, id: command.Id);
    }
}
