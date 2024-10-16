using ErrorOr;
using IHolder.API.Common;
using IHolder.Application.Allocations.Divisions;
using IHolder.Application.Allocations.List;
using IHolder.Application.Allocations.Recalculations;
using IHolder.Application.Allocations.UpdateByAsset;
using IHolder.Application.Allocations.UpdateByCategory;
using IHolder.Application.Allocations.UpdateByProduct;
using IHolder.Application.Common.Interfaces;
using IHolder.Contracts.Allocations;
using IHolder.Domain.Allocations;
using IHolder.SharedKernel.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IHolder.API.Allocations;

[Route("[controller]")]
public class AllocationsController(ISender _mediator, ICurrentUserProvider currentUserProvider) : IHolderControllerBase
{
    private readonly Guid _userID = currentUserProvider.GetCurrentUser().Value.Id;

    [HttpPut("category/{id}")]
    public async Task<IActionResult> Update(Guid id, AllocationByCategoryUpdateRequest request, CancellationToken ct)
    {
        AllocationByCategoryUpdateCommand command = request.ToCommand(id);

        ErrorOr<AllocationByCategory> allocationByCategory = await _mediator.Send(command, ct);

        IActionResult response = allocationByCategory.Match(allocation => base.Ok(allocation.ToResponse()), Problem);

        return response;
    }

    [HttpPut("category/divide")]
    public async Task<IActionResult> Divide(AllocationByCategoryDivideTargetPercentageRequest request, CancellationToken ct)
    {
        AllocationByCategoryDivideTargetPercentageCommand command = request.ToCommand();

        ErrorOr<PaginatedList<AllocationByCategory>> paginatedList = await _mediator.Send(command, ct);

        IActionResult response = paginatedList.Match(list => base.Ok(list.ToResponse()), Problem);

        return response;
    }

    [HttpPut("category/recalculate")]
    public async Task<IActionResult> Recalculate(AllocationByCategoryRecalculateRequest request, CancellationToken ct)
    {
        AllocationByCategoryRecalculateCommand command = request.ToCommand();

        ErrorOr<PaginatedList<AllocationByCategory>> paginatedList = await _mediator.Send(command, ct);

        IActionResult response = paginatedList.Match(list => base.Ok(list.ToResponse()), Problem);

        return response;
    }

    [HttpPut("product/{id}")]
    public async Task<IActionResult> Update(Guid id, AllocationByProductUpdateRequest request, CancellationToken ct)
    {
        AllocationByProductUpdateCommand command = request.ToCommand(id);

        ErrorOr<AllocationByProduct> allocationByProduct = await _mediator.Send(command, ct);

        IActionResult response = allocationByProduct.Match(allocation => base.Ok(allocation.ToResponse()), Problem);

        return response;
    }

    [HttpPut("product/divide")]
    public async Task<IActionResult> Divide(AllocationByProductDivideTargetPercentageRequest request, CancellationToken ct)
    {
        AllocationByProductDivideTargetPercentageCommand command = request.ToCommand();

        ErrorOr<PaginatedList<AllocationByProduct>> paginatedList = await _mediator.Send(command, ct);

        IActionResult response = paginatedList.Match(list => base.Ok(list.ToResponse()), Problem);

        return response;
    }

    [HttpPut("product/recalculate")]
    public async Task<IActionResult> Recalculate(AllocationByProductRecalculateRequest request, CancellationToken ct)
    {
        AllocationByProductRecalculateCommand command = request.ToCommand();

        ErrorOr<PaginatedList<AllocationByProduct>> paginatedList = await _mediator.Send(command, ct);

        IActionResult response = paginatedList.Match(list => base.Ok(list.ToResponse()), Problem);

        return response;
    }

    [HttpPut("asset/{id}")]
    public async Task<IActionResult> Update(Guid id, AllocationByAssetUpdateRequest request, CancellationToken ct)
    {
        AllocationByAssetUpdateCommand command = request.ToCommand(id);

        ErrorOr<AllocationByAsset> allocationByAsset = await _mediator.Send(command, ct);

        IActionResult response = allocationByAsset.Match(allocation => base.Ok(allocation.ToResponse()), Problem);

        return response;
    }

    [HttpPut("asset/divide")]
    public async Task<IActionResult> Divide(AllocationByAssetDivideTargetPercentageRequest request, CancellationToken ct)
    {
        AllocationByAssetDivideTargetPercentageCommand command = request.ToCommand();

        ErrorOr<PaginatedList<AllocationByAsset>> paginatedList = await _mediator.Send(command, ct);

        IActionResult response = paginatedList.Match(list => base.Ok(list.ToResponse()), Problem);

        return response;
    }

    [HttpGet("category")]
    public async Task<IActionResult> GetPaginated([FromQuery] AllocationByCategoryPaginatedListRequest request, CancellationToken ct)
    {
        AllocationByCategoriesPaginatedListQuery query = request.ToQuery(_userID);

        ErrorOr<PaginatedList<AllocationByCategory>> paginatedList = await _mediator.Send(query, ct);

        IActionResult response = paginatedList.Match(list => base.Ok(list.ToResponse()), Problem);

        return response;
    }

    [HttpGet("product")]
    public async Task<IActionResult> GetPaginated([FromQuery] AllocationByProductPaginatedListRequest request, CancellationToken ct)
    {
        AllocationByProductsPaginatedListQuery query = request.ToQuery(_userID);

        ErrorOr<PaginatedList<AllocationByProduct>> paginatedList = await _mediator.Send(query, ct);

        IActionResult response = paginatedList.Match(list => base.Ok(list.ToResponse()), Problem);

        return response;
    }

    [HttpGet("asset")]
    public async Task<IActionResult> GetPaginated([FromQuery] AllocationByAssetPaginatedListRequest request, CancellationToken ct)
    {
        AllocationByAssetsPaginatedListQuery query = request.ToQuery(_userID);

        ErrorOr<PaginatedList<AllocationByAsset>> paginatedList = await _mediator.Send(query, ct);

        IActionResult response = paginatedList.Match(list => base.Ok(list.ToResponse()), Problem);

        return response;
    }
}