using Ticketing.Domain.Entities;

namespace Ticketing.Infrastructure.Mongo.Persistence;

public sealed record EventDocument(
    Guid Id,
    string Name,
    string Category,
    string Venue,
    string City,
    DateTimeOffset StartsAt,
    decimal BasePrice,
    int Capacity,
    IReadOnlyCollection<string> Tags)
{
    public static EventDocument FromEntity(EventEntity entity) => new(
        entity.Id,
        entity.Name,
        entity.Category,
        entity.Venue,
        entity.City,
        entity.StartsAt,
        entity.BasePrice,
        entity.Capacity,
        entity.Tags);

    public EventEntity ToEntity() => new()
    {
        Id = Id,
        Name = Name,
        Category = Category,
        Venue = Venue,
        City = City,
        StartsAt = StartsAt,
        BasePrice = BasePrice,
        Capacity = Capacity,
        Tags = Tags
    };
}
