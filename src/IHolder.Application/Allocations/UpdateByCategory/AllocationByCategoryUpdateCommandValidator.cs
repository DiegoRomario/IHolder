using FluentValidation;

namespace IHolder.Application.Allocations.UpdateByCategory;

public class AllocationByCategoryUpdateCommandValidator : AbstractValidator<AllocationByCategoryUpdateCommand>
{
    public AllocationByCategoryUpdateCommandValidator()
    {

        RuleFor(x => x.Id).NotEqual(Guid.Empty)
                          .WithMessage("Allocation by category id must not be empty.");

        RuleFor(x => x.TargetPercentage).InclusiveBetween(0, 100);
    }
}