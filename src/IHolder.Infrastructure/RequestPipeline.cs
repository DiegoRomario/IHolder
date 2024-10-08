﻿using IHolder.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace IHolder.Infrastructure;

public static class RequestPipeline
{
    public static IApplicationBuilder AddInfrastructureMiddleware(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<EventualConsistencyMiddleware>();
        return builder;
    }
}