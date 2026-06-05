namespace Ticketing.Domain.Entities;

public sealed record TicketEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid EventId { get; init; }
    public Guid UserId { get; init; }
    public string SeatCode { get; init; } = string.Empty;
    public decimal AmountPaid { get; init; }
    public DateTimeOffset PurchasedAt { get; init; } = DateTimeOffset.UtcNow;
    public string DeliveryReference { get; init; } = string.Empty;
}
