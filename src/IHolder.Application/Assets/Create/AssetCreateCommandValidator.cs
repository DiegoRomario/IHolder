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

        RuleFor(x => x.Name).NotEmpty()
                                   .MaximumLength(80);

        RuleFor(x => x.Description).NotEmpty()
                               .MaximumLength(600);

        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);

        RuleFor(x => x.Ticker).MustAsync(ValidateTicker)
                           .WithMessage("This Asset already exists in the system");

        RuleFor(x => x.Name).MustAsync(ValidateName)
                                   .WithMessage("This Asset already exists in the system");

        RuleFor(x => x.ProductId).NotEqual(Guid.Empty)
                                  .WithMessage("ProductId must not be empty.");

        When(x => x.ProductId != Guid.Empty, () =>
        {
            RuleFor(x => x.ProductId).MustAsync(ValidateProductId)
                                     .WithMessage("ProductId '{PropertyValue}' does not refer to an existing product.");
        });
    }

    private async Task<bool> ValidateName(string name, CancellationToken ct = default)
    {
        return await _assetRepository.ExistsByPredicateAsync(a => a.Name == name, ct) is false;
    }

    private async Task<bool> ValidateTicker(string ticker, CancellationToken ct = default)
    {
        return await _assetRepository.ExistsByPredicateAsync(a => a.Ticker == ticker, ct) is false;
    }

    private async Task<bool> ValidateProductId(Guid productId, CancellationToken ct = default)
    {
        return await _productRepository.ExistsByPredicateAsync(p => p.Id == productId, ct);
    }
}