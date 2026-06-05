using Ticketing.Domain.Tickets;

namespace Ticketing.Application.Tickets;

public sealed record PurchaseTicketRequest(
    string EventId,
    string UserId,
    int Quantity,
    string? SeatPreference);

public sealed record TicketDto(
    string Id,
    string EventId,
    string UserId,
    string PurchaseId,
    string SeatLabel,
    decimal Price,
    TicketStatus Status,
    DateTimeOffset PurchasedAt,
    string BarcodeToken);

public sealed record TicketDownloadDto(string TicketId, string FileName, byte[] Content, string ContentType);
