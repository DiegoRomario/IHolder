using IHolder.Application.Categories.Create;
using IHolder.Application.Categories.Update;
using IHolder.Domain.Categories;

namespace IHolder.Application.Categories.Mappers;

public static class CategoryCommandsMapping
{
    public static Category ToCategoryEntity(this CategoryCreateCommand command)
    {
        return new Category(command.Description, command.Details);
    }

    public static Category ToCategoryEntity(this CategoryUpdateCommand command)
    {
        return new Category(command.Description, command.Details, id: command.Id);
    }
}
