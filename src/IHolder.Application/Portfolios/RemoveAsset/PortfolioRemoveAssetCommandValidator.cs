using FluentValidation;
using IHolder.Application.Portfolios.AddAsset;

namespace IHolder.Application.Portfolios.RemoveAsset;

public class PortfolioRemoveAssetCommandValidator : AbstractValidator<PortfolioAddAssetCommand>
{
    public PortfolioRemoveAssetCommandValidator()
    {
        RuleFor(x => x.AssetId).NotEqual(Guid.Empty)
                               .WithMessage("Asset Id must not be empty.");
    }
}