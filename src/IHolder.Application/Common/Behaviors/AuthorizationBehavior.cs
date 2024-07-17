using ErrorOr;
using IHolder.Application.Common.Auth;
using IHolder.Application.Common.Interfaces;
using MediatR;
using System.Reflection;

namespace IHolder.Application.Common.Behaviors;
public class AuthorizationBehavior<TRequest, TResponse>(ICurrentUserProvider _currentUserProvider) : IPipelineBehavior<TRequest, TResponse>
                                                                                                     where TRequest : IRequest<TResponse>
                                                                                                     where TResponse : IErrorOr
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizationAttributes = request.GetType().GetCustomAttributes<AuthorizationAttribute>().ToList();

        if (authorizationAttributes.Count == 0) return await next();

        var currentUserResult = _currentUserProvider.GetCurrentUser();

        if (currentUserResult.IsError) return (dynamic)currentUserResult.Errors;

        var requiredPermissions = authorizationAttributes.SelectMany(attribute => attribute.Permissions?.Split(',') ?? [])
                                                         .ToList();

        var currentUser = currentUserResult.Value;

        if (requiredPermissions.Except(currentUser.Permissions).Any())
            return (dynamic)Error.Unauthorized(description: "User is forbidden from taking this action");

        var requiredRoles = authorizationAttributes.SelectMany(authorizationAttribute => authorizationAttribute.Roles?.Split(',') ?? [])
                                                   .ToList();

        if (requiredRoles.Except(currentUser.Roles).Any())
            return (dynamic)Error.Unauthorized(description: "User role is forbidden from taking this action");

        return await next();
    }
}
