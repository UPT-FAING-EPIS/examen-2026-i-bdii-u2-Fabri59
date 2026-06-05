using Ticketing.Domain.Events;

namespace Ticketing.Application.Events;

public sealed record EventSearchRequest(
    string? Query,
    EventCategory? Category,
    string? City,
    DateOnly? From,
    DateOnly? To);

public sealed record EventSummaryDto(
    string Id,
    string Slug,
    string Title,
    EventCategory Category,
    DateTimeOffset StartsAt,
    string City,
    decimal BasePrice,
    int AvailableSeats);

public sealed record EventDetailDto(
    string Id,
    string Slug,
    string Title,
    string Description,
    EventCategory Category,
    DateTimeOffset StartsAt,
    string VenueId,
    string City,
    decimal BasePrice,
    int Capacity,
    int AvailableSeats,
    IReadOnlyList<string> Artists);

public sealed record CreateEventRequest(
    string Title,
    string Description,
    EventCategory Category,
    DateTimeOffset StartsAt,
    string VenueId,
    string City,
    decimal BasePrice,
    int Capacity,
    IReadOnlyList<string> Artists);
