using FluentValidation;

namespace IHolder.Application.Portfolios.UpdateAsset;

public class PortfolioUpdateAssetCommandValidator : AbstractValidator<PortfolioUpdateAssetCommand>
{
    public PortfolioUpdateAssetCommandValidator()
    {
        RuleFor(a => a.AveragePrice).GreaterThan(0);
        RuleFor(a => a.Quantity).GreaterThan(0);
    }
}