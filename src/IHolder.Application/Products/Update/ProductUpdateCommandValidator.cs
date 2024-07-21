using FluentValidation;
using IHolder.Application.Common;

namespace IHolder.Application.Products.Update;

public class UpdateCreateCommandValidator : AbstractValidator<ProductUpdateCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    public UpdateCreateCommandValidator(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
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

    private async Task<bool> ValidateDescription(ProductUpdateCommand ProductUpdateCommand, string description, CancellationToken token = default)
    {
        var existingProduct = await _productRepository.GetByDescriptionAsync(description);

        if (existingProduct is not null) return existingProduct.Id == ProductUpdateCommand.Id;

        return existingProduct is null;
    }

    private async Task<bool> ValidateCategoryId(Guid categoryId, CancellationToken token = default)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(categoryId);
        return existingCategory != null;
    }
}