using FluentValidation;
using IHolder.Application.Common.Interfaces;

namespace IHolder.Application.Users.Update;

public class UserUpdateCommandValidator : AbstractValidator<UserUpdateCommand>
{
    private readonly IUserRepository _UserRepository;
    public UserUpdateCommandValidator(IUserRepository UserRepository)
    {
        _UserRepository = UserRepository;
        RuleFor(x => x.FirstName).NotEmpty()
                                 .MinimumLength(2)
                                 .MaximumLength(80);

        RuleFor(x => x.LastName).NotEmpty()
                                .MinimumLength(2)
                                .MaximumLength(80);

        RuleFor(x => x.Email).EmailAddress()
                             .MaximumLength(80);

        // Only validate password if it is not null
        When(x => !string.IsNullOrEmpty(x.Password), () =>
        {
            RuleFor(x => x.Password).NotEmpty()
                                    .MinimumLength(8)
                                    .MaximumLength(40)
                                    .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                                    .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                                    .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                                    .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.PasswordConfirmation).Equal(x => x.Password).WithMessage("Password confirmation must match the password.");
        });

        RuleFor(x => x.Email)
               .MustAsync(ValidateEmailAddress)
               .WithMessage("This e-mail address is already taken");

    }

    private async Task<bool> ValidateEmailAddress(UserUpdateCommand UserUpdateCommand, string email, CancellationToken ct = default)
    {
        var existingUser = await _UserRepository.GetByPredicateAsync(u => u.Email == email, ct);

        if (existingUser is not null) return existingUser.Id == UserUpdateCommand.Id;

        return existingUser is null;
    }
}