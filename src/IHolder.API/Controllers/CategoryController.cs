using ErrorOr;
using IHolder.API.Mappers.Categories;
using IHolder.Application.Categories.Create;
using IHolder.Application.Categories.List;
using IHolder.Application.Categories.Update;
using IHolder.Contracts.Categories;
using IHolder.Domain.Categories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IHolder.API.Controllers;

[Route("[controller]")]
[Authorize]
public class CategoryController(ISender _mediator) : IHolderControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        CategoryGetByIdQuery command = new(id);

        ErrorOr<Category> categoryResult = await _mediator.Send(command);

        IActionResult response = categoryResult.Match(category => base.Ok(category.ToCategoryResponse()), Problem);

        return response;
    }

    [HttpPost()]
    public async Task<IActionResult> Create(CategoryCreateRequest request)
    {
        CategoryCreateCommand command = request.ToCategoryCreateCommand();

        ErrorOr<Category> createCategoryResult = await _mediator.Send(command);

        IActionResult response = createCategoryResult.Match(category => base.Ok(category.ToCategoryResponse()), Problem);

        return response;
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, CategoryUpdateRequest request)
    {
        CategoryUpdateCommand command = request.ToCategoryUpdateCommand(id);

        ErrorOr<Category> updateCategoryResult = await _mediator.Send(command);

        IActionResult response = updateCategoryResult.Match(category => base.Ok(category.ToCategoryResponse()), Problem);

        return response;
    }
}