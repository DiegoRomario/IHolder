using IHolder.Application.Categories.Create;
using IHolder.Application.Categories.Update;
using IHolder.Domain.Categories;

namespace IHolder.Application.Categories.Mappers;

public static class CategoryCommandsMapping
{
    public static Category ToEntity(this CategoryCreateCommand command)
    {
        return new Category(command.Name, command.Description);
    }

    public static Category ToEntity(this CategoryUpdateCommand command)
    {
        return new Category(command.Name, command.Description, id: command.Id);
    }
}
