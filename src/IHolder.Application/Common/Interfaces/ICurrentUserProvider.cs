using ErrorOr;
using IHolder.Application.Common.Models;

namespace IHolder.Application.Common.Interfaces;
public interface ICurrentUserProvider
{
    ErrorOr<CurrentUser> GetCurrentUser();
}