using FluentValidation;

namespace IHolder.Application.Portfolios.Create;

public class PortfolioCreateCommandValidator : AbstractValidator<PortfolioCreateCommand>
{
    public PortfolioCreateCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty()
                            .MaximumLength(80);

        RuleFor(x => x.UserId).NotEqual(Guid.Empty)
                              .WithMessage("UserId must not be empty.");
    }
}