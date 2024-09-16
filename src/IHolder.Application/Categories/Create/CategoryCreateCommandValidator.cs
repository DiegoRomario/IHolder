using FluentValidation;
using IHolder.Application.Common;

namespace IHolder.Application.Categories.Create;

public class CategoryCreateCommandValidator : AbstractValidator<CategoryCreateCommand>
{
    private readonly ICategoryRepository _repository;
    public CategoryCreateCommandValidator(ICategoryRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Name).NotEmpty()
                            .MaximumLength(80);

        RuleFor(x => x.Description).NotEmpty()
                                   .MaximumLength(600);

        RuleFor(x => x.Name).MustAsync(ValidateName)
                                   .WithMessage("This category already exists in the system");
    }

    private async Task<bool> ValidateName(CategoryCreateCommand categoryUpdateCommand, string name, CancellationToken ct = default)
    {
        return await _repository.ExistsByPredicateAsync(c => c.Name == name, ct);
    }
}