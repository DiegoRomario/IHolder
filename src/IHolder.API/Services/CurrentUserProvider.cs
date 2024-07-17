using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Application.Common.Models;
using System.Security.Claims;
using Throw;

namespace IHolder.API.Services;

public class CurrentUserProvider(IHttpContextAccessor _httpContextAccessor) : ICurrentUserProvider
{
    public ErrorOr<CurrentUser> GetCurrentUser()
    {
        _httpContextAccessor.HttpContext.ThrowIfNull();

        var id = GetClaimValues("id")?.Select(Guid.Parse).FirstOrDefault();

        if (id is null || id == Guid.Empty)
            return Error.Unauthorized(description: "Authentication is required to access this resource.");

        var permissions = GetClaimValues("permissions");
        var roles = GetClaimValues(ClaimTypes.Role);

        return new CurrentUser(Id: id.Value, Permissions: permissions, Roles: roles);
    }

    private IReadOnlyList<string> GetClaimValues(string claimType)
    {
        return _httpContextAccessor.HttpContext!.User.Claims.Where(claim => claim.Type == claimType).Select(claim => claim.Value).ToList();
    }
}
