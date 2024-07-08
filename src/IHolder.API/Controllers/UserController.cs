using ErrorOr;
using IHolder.API.Mappers.Users;
using IHolder.Application.Common.Auth;
using IHolder.Application.Users.Login;
using IHolder.Application.Users.Register;
using IHolder.Contracts.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IHolder.API.Controllers;

[Route("[controller]")]
[AllowAnonymous]
public class UserController(ISender _mediator) : IHolderControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterRequest request)
    {
        UserRegisterCommand command = new(request.FirstName, request.LastName, request.Email, request.Password);

        ErrorOr<AuthenticationResult> authenticationResult = await _mediator.Send(command);

        IActionResult response = authenticationResult.Match(authResult => base.Ok(authResult.ToAuthenticationResponse()), Problem);

        return response;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        LoginQuery query = new(request.Email, request.Password);

        ErrorOr<AuthenticationResult> authenticationResult = await _mediator.Send(query);

        if (authenticationResult.IsError && authenticationResult.FirstError == AuthenticationErrors.InvalidCredentials)
            return Problem(detail: authenticationResult.FirstError.Description, statusCode: StatusCodes.Status401Unauthorized);

        return authenticationResult.Match(authResult => Ok(authResult.ToAuthenticationResponse()), Problem);
    }
}