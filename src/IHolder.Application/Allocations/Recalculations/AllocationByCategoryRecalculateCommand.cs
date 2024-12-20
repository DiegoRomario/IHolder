﻿using ErrorOr;
using IHolder.Domain.Allocations;
using IHolder.SharedKernel.DTO;
using MediatR;

namespace IHolder.Application.Allocations.Recalculations;

public record AllocationByCategoryRecalculateCommand(
    int PageNumber,
    short PageSize) : PaginatedFilter(PageNumber, PageSize), IRequest<ErrorOr<PaginatedList<AllocationByCategory>>>;
