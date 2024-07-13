﻿using FluentValidation;
using IHolder.Application.Common;

namespace IHolder.Application.Categories.Create;

public class CategoryCreateCommandValidator : AbstractValidator<CategoryCreateCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    public CategoryCreateCommandValidator(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;

        RuleFor(x => x.Description).NotEmpty()
                                   .MaximumLength(80);

        RuleFor(x => x.Details).NotEmpty()
                               .MaximumLength(600);

        RuleFor(x => x.Description)
               .MustAsync(ValidateDescription)
               .WithMessage("This category already exists in the system");

    }

    private async Task<bool> ValidateDescription(CategoryCreateCommand categoryUpdateCommand, string description, CancellationToken token = default)
    {
        var existingCategory = await _categoryRepository.GetByDescriptionAsync(description);
        return existingCategory is null;
    }
}