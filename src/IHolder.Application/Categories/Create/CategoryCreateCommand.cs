using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Categories;
using MediatR;

namespace IHolder.Application.Categories.Create;

[Authorization]
public record CategoryCreateCommand(string Description, string Details) : IRequest<ErrorOr<Category>>;
