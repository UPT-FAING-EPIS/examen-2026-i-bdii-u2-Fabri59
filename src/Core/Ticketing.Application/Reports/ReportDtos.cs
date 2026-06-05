namespace Ticketing.Application.Reports;

public sealed record SalesReportRequest(DateOnly From, DateOnly To, string? EventId);

public sealed record SalesReportDto(
    string Scope,
    int TicketsSold,
    decimal Revenue,
    int UniqueAttendees,
    IReadOnlyList<string> Notes);
