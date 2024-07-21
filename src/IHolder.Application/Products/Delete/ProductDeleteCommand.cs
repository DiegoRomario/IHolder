using ErrorOr;
using IHolder.Application.Common.Auth;
using MediatR;

namespace IHolder.Application.Products.Delete;

[Authorization]
public record ProductDeleteCommand(Guid Id) : IRequest<ErrorOr<Deleted>>;
