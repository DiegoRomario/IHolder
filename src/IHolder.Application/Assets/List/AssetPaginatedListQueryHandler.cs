using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Assets;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Assets.List;

public class AssetPaginatedListQueryHandler(IAssetRepository _repository) : IRequestHandler<AssetPaginatedListQuery, ErrorOr<PaginatedList<Asset>>>
{
    public async Task<ErrorOr<PaginatedList<Asset>>> Handle(AssetPaginatedListQuery request, CancellationToken ct)
    {
        return await _repository.GetPaginatedAsync(request.Filter, ct);
    }
}