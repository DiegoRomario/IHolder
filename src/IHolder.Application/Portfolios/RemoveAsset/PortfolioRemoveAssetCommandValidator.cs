using FluentValidation;

namespace IHolder.Application.Portfolios.AddAsset;

public class PortfolioRemoveAssetCommandValidator : AbstractValidator<PortfolioAddAssetCommand>
{
    public PortfolioRemoveAssetCommandValidator()
    {
        RuleFor(x => x.AssetId).NotEqual(Guid.Empty)
                               .WithMessage("Asset Id must not be empty.");
    }
}