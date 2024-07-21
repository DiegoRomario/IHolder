﻿using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Domain.Enumerators;
using IHolder.Domain.Products;
using MediatR;

namespace IHolder.Application.Products.Create;

[Authorization]
public record ProductCreateCommand(
    string Description,
    string Details,
    Guid CategoryId,
    Risk Risk) : IRequest<ErrorOr<Product>>;

