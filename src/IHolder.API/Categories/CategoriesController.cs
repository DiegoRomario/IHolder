using ErrorOr;
using IHolder.API.Common;
using IHolder.Application.Categories.Create;
using IHolder.Application.Categories.Delete;
using IHolder.Application.Categories.List;
using IHolder.Application.Categories.Update;
using IHolder.Contracts.Categories;
using IHolder.Domain.Categories;
using IHolder.SharedKernel.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IHolder.API.Categories;

[Authorize]
[Route("[controller]")]
public class CategoriesController(ISender _mediator) : IHolderControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken ct)
    {
        CategoryGetByIdQuery command = new(id);

        ErrorOr<Category> category = await _mediator.Send(command, ct);

        IActionResult response = category.Match(category => base.Ok(category.ToResponse()), Problem);

        return response;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaginated([FromQuery] CategoryPaginatedListRequest request, CancellationToken ct)
    {
        CategoriesPaginatedListQuery query = request.ToQuery();

        ErrorOr<PaginatedList<Category>> paginatedList = await _mediator.Send(query, ct);

        IActionResult response = paginatedList.Match(list => base.Ok(list.ToResponse()), Problem);

        return response;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateRequest request, CancellationToken ct)
    {
        CategoryCreateCommand command = request.ToCommand();

        ErrorOr<Category> category = await _mediator.Send(command, ct);

        IActionResult response = category.Match(category => base.CreatedAtAction(nameof(Get), new { id = category.Id }, category.ToResponse()), Problem);

        return response;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, CategoryUpdateRequest request, CancellationToken ct)
    {
        CategoryUpdateCommand command = request.ToCommand(id);

        ErrorOr<Category> category = await _mediator.Send(command, ct);

        IActionResult response = category.Match(category => base.Ok(category.ToResponse()), Problem);

        return response;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        CategoryDeleteCommand command = new(id);

        ErrorOr<Deleted> result = await _mediator.Send(command, ct);

        return result.Match(_ => NoContent(), Problem);
    }
}