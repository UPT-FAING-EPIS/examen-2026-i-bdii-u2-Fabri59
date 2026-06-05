using Ticketing.Application.Tickets;

namespace Ticketing.Application.Services;

public sealed class TicketOperationsService
{
    public Task<TicketDto> PurchaseAsync(PurchaseTicketRequest request, CancellationToken cancellationToken = default)
    {
        var ticket = new TicketDto(
            Guid.NewGuid().ToString(),
            request.EventId,
            request.UserId,
            Guid.NewGuid().ToString(),
            request.SeatPreference ?? "GENERAL",
            49.99m * Math.Max(1, request.Quantity),
            Ticketing.Domain.Tickets.TicketStatus.Paid,
            DateTimeOffset.UtcNow,
            Convert.ToBase64String(Guid.NewGuid().ToByteArray()));

        return Task.FromResult(ticket);
    }

    public byte[] BuildTicketFile(Guid ticketId)
        => System.Text.Encoding.UTF8.GetBytes($"Ticket {ticketId:D}\nGenerated at {DateTimeOffset.UtcNow:O}");
}
