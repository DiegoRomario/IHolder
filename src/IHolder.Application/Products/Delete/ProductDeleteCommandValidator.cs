using FluentValidation;

namespace IHolder.Application.Products.Delete;

public class ProductDeleteCommandValidator : AbstractValidator<ProductDeleteCommand>
{
    public ProductDeleteCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty)
                          .WithMessage("Id must not be empty.");
    }
}