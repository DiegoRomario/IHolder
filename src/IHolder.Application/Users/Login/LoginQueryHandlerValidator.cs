using FluentValidation;

namespace IHolder.Application.Users.Login;

public class LoginQueryHandlerValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryHandlerValidator()
    {

        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}