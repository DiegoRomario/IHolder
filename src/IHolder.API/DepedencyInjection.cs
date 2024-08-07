﻿using IHolder.API.Common.Swagger;
using IHolder.API.Users;
using IHolder.Application.Common.Interfaces;

namespace IHolder.API;

public static class DepedencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerConfiguration();
        services.AddProblemDetails();
        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUserProvider, CurrentUserProviderService>();

        return services;
    }
}

