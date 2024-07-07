using FluentValidation;

namespace IHolder.Application.Users.Register;
public class UserRegisterCommandValidator : AbstractValidator<UserRegisterCommand>
{
    public UserRegisterCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty()
                                 .MinimumLength(2)
                                 .MaximumLength(80);

        RuleFor(x => x.LastName).NotEmpty()
                                .MinimumLength(2)
                                .MaximumLength(80);

        RuleFor(x => x.Email).EmailAddress()
                             .MaximumLength(80);

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
                                .MinimumLength(8)
                                .MaximumLength(40)
                                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                                .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
    }
}