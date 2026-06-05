using Ticketing.Domain.Users;

namespace Ticketing.Application.Security;

public sealed record RegisterRequest(string Email, string FullName, string Password, UserRole Role);

public sealed record LoginRequest(string Email, string Password);

public sealed record AuthResultDto(string UserId, string Email, string FullName, UserRole Role, string AccessToken);
