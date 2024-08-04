using ErrorOr;
using IHolder.Application.Common.Interfaces;
using IHolder.Domain.Users;
using MediatR;

namespace IHolder.Application.Users.Update;

public class UserUpdateCommandHandler(IUserRepository _repository) : IRequestHandler<UserUpdateCommand, ErrorOr<User>>
{
    public async Task<ErrorOr<User>> Handle(UserUpdateCommand request, CancellationToken ct)
    {
        var user = await _repository.GetByIdAsync(request.Id, ct);

        if (user is null)
            return Error.Conflict(description: "User not found");

        user.UpdateUserDetails(request.FirstName, request.LastName, request.Email, request.Password);

        await _repository.UpdateAsync(user, ct);

        user = await _repository.GetByIdAsync(request.Id, ct);

        if (user == null) return Error.Conflict(description: "Failed to retrieve the updated User.");

        return user;
    }
}
