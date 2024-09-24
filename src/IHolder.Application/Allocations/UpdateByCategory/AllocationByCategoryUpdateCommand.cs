using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Allocations;
using MediatR;

namespace IHolder.Application.Allocations.UpdateByCategory;

[Authorization]
public record AllocationByCategoryUpdateCommand(Guid Id, decimal TargetPercentage) : IRequest<ErrorOr<AllocationByCategory>>;
