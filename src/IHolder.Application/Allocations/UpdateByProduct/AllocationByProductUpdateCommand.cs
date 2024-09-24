using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Allocations;
using MediatR;

namespace IHolder.Application.Allocations.UpdateByProduct;

[Authorization]
public record AllocationByProductUpdateCommand(Guid Id, decimal TargetPercentage) : IRequest<ErrorOr<AllocationByProduct>>;
