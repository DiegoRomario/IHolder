using IHolder.Application.Common;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Common;
using IHolder.Infrastructure.Allocations;
using IHolder.Infrastructure.Assets;
using IHolder.Infrastructure.Authentication;
using IHolder.Infrastructure.Categories;
using IHolder.Infrastructure.Database;
using IHolder.Infrastructure.Portfolios;
using IHolder.Infrastructure.Products;
using IHolder.Infrastructure.Services;
using IHolder.Infrastructure.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IHolder.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddAuthentication(configuration)
                       .AddPersistence(configuration);
    }

    public static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IAssetQuoteService, StockQuoteService>(client =>
        {
            // TODO: LOGS
            string baseAddress = configuration["ExternalServices:StockQuote"]
                ?? throw new ArgumentNullException(nameof(baseAddress), "StockQuote base address is required.");

            string userAgent = configuration["ExternalServices:UserAgent"]
                ?? throw new ArgumentNullException(nameof(userAgent), "UserAgent is required.");

            client.BaseAddress = new Uri(baseAddress);
            client.Timeout = TimeSpan.FromSeconds(30);
            client.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
        })
        .SetHandlerLifetime(TimeSpan.FromMinutes(5));

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IHolderDbContext>(options => options.UseSqlServer(configuration["Database:IHolderConnectionString"]));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IAssetRepository, AssetRepository>();
        services.AddScoped<IPortfolioRepository, PortfolioRepository>();
        services.AddScoped<IAllocationRepository, AllocationRepository>();
        services.AddScoped<IAllocationByCategoryRepository, AllocationByCategoryRepository>();
        services.AddScoped<IAllocationByProductRepository, AllocationByProductRepository>();
        services.AddScoped<IAllocationByAssetRepository, AllocationByAssetRepository>();

        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.Section, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                });

        return services;
    }
}