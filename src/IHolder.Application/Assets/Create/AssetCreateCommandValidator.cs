using FluentValidation;
using IHolder.Application.Common.Interfaces;

namespace IHolder.Application.Assets.Create;

public class AssetCreateCommandValidator : AbstractValidator<AssetCreateCommand>
{
    private readonly IAssetRepository _assetRepository;
    private readonly IProductRepository _productRepository;

    public AssetCreateCommandValidator(IAssetRepository repository, IProductRepository productRepository)
    {
        _assetRepository = repository;
        _productRepository = productRepository;

        RuleFor(x => x.Ticker).NotEmpty()
                              .MaximumLength(80);

        RuleFor(x => x.Description).NotEmpty()
                                   .MaximumLength(80);

        RuleFor(x => x.Details).NotEmpty()
                               .MaximumLength(600);

        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);

        RuleFor(x => x.Ticker).MustAsync(ValidateTicker)
                           .WithMessage("This Asset already exists in the system");

        RuleFor(x => x.Description).MustAsync(ValidateDescription)
                                   .WithMessage("This Asset already exists in the system");

        RuleFor(x => x.ProductId).NotEqual(Guid.Empty)
                                  .WithMessage("ProductId must not be empty.");

        When(x => x.ProductId != Guid.Empty, () =>
        {
            RuleFor(x => x.ProductId).MustAsync(ValidateProductId)
                                     .WithMessage("ProductId '{PropertyValue}' does not refer to an existing product.");
        });
    }

    private async Task<bool> ValidateDescription(string description, CancellationToken token = default)
    {
        return await _assetRepository.ExistsByPredicateAsync(a => a.Description == description) is false;
    }

    private async Task<bool> ValidateTicker(string ticker, CancellationToken token = default)
    {
        return await _assetRepository.ExistsByPredicateAsync(a => a.Ticker == ticker) is false;
    }

    private async Task<bool> ValidateProductId(Guid productId, CancellationToken token = default)
    {
        return await _productRepository.ExistsByIdAsync(productId);
    }
}