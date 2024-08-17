using FluentValidation;
using IHolder.Application.Common.Interfaces;

namespace IHolder.Application.Portfolios.UpdateAsset;
public class PortfolioUpdateAssetCommandValidator : AbstractValidator<PortfolioUpdateAssetCommand>
{
    private readonly IAssetRepository _assetRepository;
    private readonly IPortfolioRepository _portfolioRepository;

    public PortfolioUpdateAssetCommandValidator(IAssetRepository assetRepository, IPortfolioRepository portfolioRepository)
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
                                   .WithMessage("An Asset with this Ticker already exists in the portfolio");
        });
    }

    private async Task<bool> ValidateAssetId(Guid assetId, CancellationToken ct = default)
    {
        return await _assetRepository.ExistsByPredicateAsync(a => a.Id == assetId, ct);
    }

    private async Task<bool> ValidateAssetInPortfolio(PortfolioUpdateAssetCommand command, Guid assetId, CancellationToken ct = default)
    {
        return await _portfolioRepository.ExistsAssetByPredicateAsync(a => a.AssetId == assetId && a.PortfolioId == command.PortfolioId && a.Id != command.Id, ct) is false;
    }
}