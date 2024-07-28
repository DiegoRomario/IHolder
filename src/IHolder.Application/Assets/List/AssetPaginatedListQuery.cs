using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Assets;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Assets.List;

[Authorization]
public record AssetPaginatedListQuery(AssetPaginatedListFilter Filter) : IRequest<ErrorOr<PaginatedList<Asset>>>;