using FluentValidation;
using IHolder.Application.Common;

namespace IHolder.Application.Categories.Update;

public class UpdateCreateCommandValidator : AbstractValidator<CategoryUpdateCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    public UpdateCreateCommandValidator(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
        RuleFor(x => x.Name).NotEmpty().MaximumLength(80);

        RuleFor(x => x.Description).NotEmpty().MaximumLength(600);

        RuleFor(x => x.Name)
                       .MustAsync(ValidateName)
                       .WithMessage("This category already exists in the system");
    }

    private async Task<bool> ValidateName(CategoryUpdateCommand categoryUpdateCommand, string name, CancellationToken ct = default)
    {
        var existingCategory = await _categoryRepository.GetByNameAsync(name, ct);

        if (existingCategory is not null) return existingCategory.Id == categoryUpdateCommand.Id;

        return existingCategory is null;
    }
}