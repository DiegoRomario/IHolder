using FluentValidation;

namespace IHolder.Application.Allocations.UpdateByAsset;

public class AllocationByAssetUpdateCommandValidator : AbstractValidator<AllocationByAssetUpdateCommand>
{
    public AllocationByAssetUpdateCommandValidator()
    {

        RuleFor(x => x.Id).NotEqual(Guid.Empty)
                          .WithMessage("Allocation by asset id must not be empty.");

        RuleFor(x => x.TargetPercentage).InclusiveBetween(0, 100);
    }
}