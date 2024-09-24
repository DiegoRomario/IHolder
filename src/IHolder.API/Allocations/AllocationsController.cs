﻿using ErrorOr;
using IHolder.API.Common;
using IHolder.Application.Allocations.UpdateByCategory;
using IHolder.Contracts.Allocations;
using IHolder.Domain.Allocations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IHolder.API.Allocations;

[Route("[controller]")]
public class AllocationsController(ISender _mediator) : IHolderControllerBase
{

    [HttpPut("category/{id}")]
    public async Task<IActionResult> Update(Guid id, AllocationByCategoryUpdateRequest request, CancellationToken ct)
    {
        AllocationByCategoryUpdateCommand command = request.ToCommand(id);

        ErrorOr<AllocationByCategory> allocationByCategory = await _mediator.Send(command, ct);

        IActionResult response = allocationByCategory.Match(allocation => base.Ok(allocation.ToResponse()), Problem);

        return response;
    }
}