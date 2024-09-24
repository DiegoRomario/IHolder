using FluentValidation;

namespace IHolder.Application.Allocations.UpdateByProduct;

public class AllocationByProductUpdateCommandValidator : AbstractValidator<AllocationByProductUpdateCommand>
{
    public AllocationByProductUpdateCommandValidator()
    {

        RuleFor(x => x.Id).NotEqual(Guid.Empty)
                          .WithMessage("Allocation by product id must not be empty.");

        RuleFor(x => x.TargetPercentage).InclusiveBetween(0, 100);
    }
}