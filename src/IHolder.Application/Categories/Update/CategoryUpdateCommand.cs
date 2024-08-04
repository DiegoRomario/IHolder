using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Categories;
using MediatR;

namespace IHolder.Application.Categories.Update;

[Authorization]
public record CategoryUpdateCommand(Guid Id, string Name, string Description) : IRequest<ErrorOr<Category>>;
