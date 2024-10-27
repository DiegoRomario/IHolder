using FluentValidation;
using IHolder.Application.Categories.Delete;

namespace IHolder.Application.Assets.Delete;

public class AssetDeleteCommandValidator : AbstractValidator<CategoryDeleteCommand>
{
    public AssetDeleteCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty)
                          .WithMessage("Id must not be empty.");
    }
}