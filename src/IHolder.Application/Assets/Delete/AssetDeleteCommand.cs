using ErrorOr;
using IHolder.Application.Common.Auth;
using MediatR;

namespace IHolder.Application.Assets.Delete;

[Authorization]
public record AssetDeleteCommand(Guid Id) : IRequest<ErrorOr<Deleted>>;
