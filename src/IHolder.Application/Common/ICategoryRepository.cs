﻿using IHolder.Application.Categories.List;
using IHolder.Domain.Categories;
using IHolder.SharedKernel.DTO;

namespace IHolder.Application.Common;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid Id);
    Task<Category?> GetByDescriptionAsync(string description);
    Task<PaginatedList<Category>> GetPaginatedAsync(CategoryPaginatedListFilter filter);
    Task<bool> ExistsByIdAsync(Guid Id);
    Task AddAsync(Category category);
    Task UpdateAsync(Category category);
}