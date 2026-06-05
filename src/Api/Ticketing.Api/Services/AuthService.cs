using Ticketing.Application.Security;
using Ticketing.Domain.Users;

namespace Ticketing.Application.Services;

public sealed class AuthService
{
    public AuthResultDto Register(RegisterRequest request)
        => new(Guid.NewGuid().ToString(), request.Email, request.FullName, request.Role, Convert.ToBase64String(Guid.NewGuid().ToByteArray()));

    public AuthResultDto Login(LoginRequest request)
        => new(Guid.NewGuid().ToString(), request.Email, request.Email, UserRole.Customer, Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
}
