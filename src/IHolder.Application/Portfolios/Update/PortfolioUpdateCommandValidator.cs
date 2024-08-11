using FluentValidation;

namespace IHolder.Application.Portfolios.Update;

public class PortfolioUpdateCommandValidator : AbstractValidator<PortfolioUpdateCommand>
{
    public PortfolioUpdateCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty()
                            .MaximumLength(80);

        RuleFor(x => x.UserId).NotEqual(Guid.Empty)
                                 .WithMessage("UserId must not be empty.");
    }
}