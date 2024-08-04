using FluentValidation;
using IHolder.Application.Common.Interfaces;

namespace IHolder.Application.Users.Create;
public class UserCreateCommandValidator : AbstractValidator<UserCreateCommand>
{
    private readonly IUserRepository _UserRepository;
    public UserCreateCommandValidator(IUserRepository userRepository)
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

        RuleFor(x => x.PasswordConfirmation).Equal(x => x.Password).WithMessage("Password confirmation must match the password.");

        RuleFor(x => x.Email)
               .MustAsync(ValidateEmailAddress)
               .WithMessage("This e-mail address is already taken");
        _UserRepository = userRepository;
    }

    private async Task<bool> ValidateEmailAddress(UserCreateCommand UserUpdateCommand, string email, CancellationToken ct = default)
    {
        return await _UserRepository.ExistsByEmailAsync(email, ct) is false;
    }
}