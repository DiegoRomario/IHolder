using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Assets;
using MediatR;

namespace IHolder.Application.Assets.List;

[Authorization]
public record AssetGetByIdQuery(Guid Id) : IRequest<ErrorOr<Asset?>>;
