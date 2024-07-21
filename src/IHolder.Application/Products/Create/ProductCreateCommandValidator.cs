using FluentValidation;
using IHolder.Application.Common;
using IHolder.Application.Common.Interfaces;

namespace IHolder.Application.Products.Create;

public class ProductCreateCommandValidator : AbstractValidator<ProductCreateCommand>
{
    private readonly IProductRepository _repository;
    private readonly ICategoryRepository _categoryRepository;
    public ProductCreateCommandValidator(IProductRepository repository, ICategoryRepository categoryRepository)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;

        RuleFor(x => x.Description).NotEmpty()
                                   .MaximumLength(80);

        RuleFor(x => x.Details).NotEmpty()
                               .MaximumLength(600);

        RuleFor(x => x.Risk).IsInEnum()
                            .WithMessage("Invalid risk value.");

        RuleFor(x => x.Description).MustAsync(ValidateDescription)
                                   .WithMessage("This Product already exists in the system");

        RuleFor(x => x.CategoryId).NotEqual(Guid.Empty)
                                  .WithMessage("CategoryId must not be empty.");

        When(x => x.CategoryId != Guid.Empty, () =>
        {
            RuleFor(x => x.CategoryId).MustAsync(ValidateCategoryId)
                                      .WithMessage("CategoryId '{PropertyValue}' does not refer to an existing category.");
        });

    }

    private async Task<bool> ValidateDescription(ProductCreateCommand ProductUpdateCommand, string description, CancellationToken token = default)
    {
        var existingProduct = await _repository.GetByDescriptionAsync(description);
        return existingProduct is null;
    }

    private async Task<bool> ValidateCategoryId(Guid categoryId, CancellationToken token = default)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(categoryId);
        return existingCategory != null;
    }
}