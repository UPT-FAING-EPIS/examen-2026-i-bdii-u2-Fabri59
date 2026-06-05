namespace Ticketing.Domain.Entities;

public sealed record EventEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public string Venue { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public DateTimeOffset StartsAt { get; init; }
    public decimal BasePrice { get; init; }
    public int Capacity { get; init; }
    public IReadOnlyCollection<string> Tags { get; init; } = Array.Empty<string>();
}
