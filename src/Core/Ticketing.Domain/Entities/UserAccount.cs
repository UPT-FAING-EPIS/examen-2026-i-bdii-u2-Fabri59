namespace Ticketing.Domain.Entities;

public sealed record UserAccountEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Role { get; init; } = "customer";
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
}
