using ErrorOr;
using IHolder.Application.Common.Auth;
using MediatR;

namespace IHolder.Application.Categories.Create;

[Authorization]
public record CategoryDeleteCommand(Guid Id) : IRequest<ErrorOr<Deleted>>;
