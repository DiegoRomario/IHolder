using FluentValidation;
using IHolder.Application.Common;

namespace IHolder.Application.Categories.Update;

public class UpdateCreateCommandValidator : AbstractValidator<CategoryUpdateCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    public UpdateCreateCommandValidator(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
        RuleFor(x => x.Description).NotEmpty().MaximumLength(80);

        RuleFor(x => x.Details).NotEmpty().MaximumLength(600);

        RuleFor(x => x.Description)
                       .MustAsync(ValidateDescription)
                       .WithMessage("This category already exists in the system");
    }

    private async Task<bool> ValidateDescription(CategoryUpdateCommand categoryUpdateCommand, string description, CancellationToken token = default)
    {
        var existingCategory = await _categoryRepository.GetByDescriptionAsync(description);

        if (existingCategory is not null) return existingCategory.Id == categoryUpdateCommand.Id;

        return existingCategory is null;
    }
}