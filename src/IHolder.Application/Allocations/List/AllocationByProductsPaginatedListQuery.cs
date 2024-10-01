using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Allocations;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Allocations.List;

[Authorization]
public record AllocationByProductsPaginatedListQuery(AllocationByProductsPaginatedListFilter Filter) : IRequest<ErrorOr<PaginatedList<AllocationByProduct>>>;