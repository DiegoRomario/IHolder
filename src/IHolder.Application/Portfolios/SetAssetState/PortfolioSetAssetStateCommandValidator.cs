using FluentValidation;
using IHolder.Application.Common.Interfaces;

namespace IHolder.Application.Portfolios.SetAssetState;

public class PortfolioSetAssetStateCommandValidator : AbstractValidator<PortfolioSetAssetStateCommand>
{
    private readonly IPortfolioRepository _portfolioRepository;

    public PortfolioSetAssetStateCommandValidator(IPortfolioRepository portfolioRepository)
    {
        _portfolioRepository = portfolioRepository;

        RuleFor(x => x.Id).NotEqual(Guid.Empty)
                          .WithMessage("Id must not be empty.");

        RuleFor(x => x.PortfolioId).NotEqual(Guid.Empty)
                  .WithMessage("PortfolioId must not be empty.");

        When(x => x.Id != Guid.Empty, () =>
        {
            RuleFor(x => x.Id).MustAsync(ValidateAssetInPortfolioId)
                              .WithMessage("Asset in portfolio Id '{PropertyValue}' does not refer to an existing asset in the user portfolio.");
        });
    }

    private async Task<bool> ValidateAssetInPortfolioId(PortfolioSetAssetStateCommand command, Guid id, CancellationToken ct = default)
    {
        return await _portfolioRepository.ExistsAssetByPredicateAsync(a => a.Id == id && a.PortfolioId == command.PortfolioId, ct);
    }
}