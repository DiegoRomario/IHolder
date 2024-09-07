using FluentValidation;
using IHolder.Application.Common.Interfaces;

namespace IHolder.Application.Portfolios.AddAsset;

public class PortfolioAddAssetCommandValidator : AbstractValidator<PortfolioAddAssetCommand>
{
    private readonly IAssetRepository _assetRepository;
    private readonly IPortfolioRepository _portfolioRepository;
    public PortfolioAddAssetCommandValidator(IAssetRepository assetRepository, IPortfolioRepository portfolioRepository)
    {
        _assetRepository = assetRepository;
        _portfolioRepository = portfolioRepository;

        RuleFor(a => a.AveragePrice).GreaterThan(0);
        RuleFor(a => a.Quantity).GreaterThan(0);

        RuleFor(x => x.AssetId).NotEqual(Guid.Empty)
                               .WithMessage("AssetId must not be empty.");

        When(x => x.AssetId != Guid.Empty, () =>
        {
            RuleFor(x => x.AssetId).MustAsync(ValidateAssetId)
                                      .WithMessage("AssetId '{PropertyValue}' does not refer to an existing asset.");

            RuleFor(x => x.AssetId).MustAsync(ValidateAssetInPortfolio)
                                   .WithMessage("An Asset with this Ticker already exists in the Portfolio");
        });
    }

    private async Task<bool> ValidateAssetId(Guid assetId, CancellationToken ct = default)
    {
        return await _assetRepository.ExistsByPredicateAsync(a => a.Id == assetId, ct);
    }

    private async Task<bool> ValidateAssetInPortfolio(PortfolioAddAssetCommand command, Guid assetId, CancellationToken ct = default)
    {
        return await _portfolioRepository.ExistsAssetInPortfolioByPredicateAsync(a => a.AssetId == assetId && a.PortfolioId == command.PortfolioId, ct) is false;
    }
}