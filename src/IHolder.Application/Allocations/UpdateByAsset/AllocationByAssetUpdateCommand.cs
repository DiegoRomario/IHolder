using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Allocations;
using MediatR;

namespace IHolder.Application.Allocations.UpdateByAsset;

[Authorization]
public record AllocationByAssetUpdateCommand(Guid Id, decimal TargetPercentage) : IRequest<ErrorOr<AllocationByAsset>>;
