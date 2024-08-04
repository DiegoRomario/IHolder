using FluentValidation;
using IHolder.Application.Common.Interfaces;

namespace IHolder.Application.Assets.Update;

public class UpdateCreateCommandValidator : AbstractValidator<AssetUpdateCommand>
{
    private readonly IAssetRepository _assetRepository;
    private readonly IProductRepository _productRepository;

    public UpdateCreateCommandValidator(IAssetRepository assetRepository, IProductRepository productRepository)
    {
        _assetRepository = assetRepository;
        _productRepository = productRepository;

        RuleFor(x => x.Ticker).NotEmpty()
                                     .MaximumLength(80);

        RuleFor(x => x.Name).NotEmpty()
                                   .MaximumLength(80);

        RuleFor(x => x.Description).NotEmpty()
                               .MaximumLength(600);

        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);

        RuleFor(x => x.Ticker).MustAsync(ValidateTicker)
                   .WithMessage("An Asset with this Ticker already exists in the system");

        RuleFor(x => x.Name).MustAsync(ValidateName)
                                   .WithMessage("An Asset with this Name already exists in the system");

        RuleFor(x => x.ProductId).NotEqual(Guid.Empty)
                                  .WithMessage("ProductId must not be empty.");

        When(x => x.ProductId != Guid.Empty, () =>
        {
            RuleFor(x => x.ProductId).MustAsync(ValidateProductId)
                                      .WithMessage("ProductId '{PropertyValue}' does not refer to an existing product.");
        });
    }

    private async Task<bool> ValidateName(AssetUpdateCommand command, string name, CancellationToken ct = default)
    {
        return await _assetRepository.ExistsByPredicateAsync(a => a.Name == name && a.Id != command.Id, ct) is false;
    }

    private async Task<bool> ValidateTicker(AssetUpdateCommand command, string ticker, CancellationToken ct = default)
    {
        return await _assetRepository.ExistsByPredicateAsync(a => a.Ticker == ticker && a.Id != command.Id, ct) is false;
    }

    private async Task<bool> ValidateProductId(Guid productId, CancellationToken ct = default)
    {
        return await _productRepository.ExistsByIdAsync(productId, ct);
    }
}