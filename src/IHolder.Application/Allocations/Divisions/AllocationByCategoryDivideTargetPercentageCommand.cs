using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Allocations;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Allocations.Divisions;

[Authorization]
public record AllocationByCategoryDivideTargetPercentageCommand(
    bool OnlyCategoriesInPortfolio,
    int PageNumber,
    short PageSize) : PaginatedFilter(PageNumber, PageSize), IRequest<ErrorOr<PaginatedList<AllocationByCategory>>>
{ };

