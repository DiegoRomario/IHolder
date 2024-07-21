using FluentValidation;

namespace IHolder.Application.Categories.Create;

public class CategoryDeleteCommandValidator : AbstractValidator<CategoryDeleteCommand>
{
    public CategoryDeleteCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty)
                          .WithMessage("Id must not be empty.");
    }
}