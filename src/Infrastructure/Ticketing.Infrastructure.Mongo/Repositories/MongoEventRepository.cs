using MongoDB.Driver;
using Ticketing.Application.Contracts.Repositories;
using Ticketing.Domain.Entities;
using Ticketing.Infrastructure.Mongo.Persistence;

namespace Ticketing.Infrastructure.Mongo.Repositories;

public sealed class MongoEventRepository : IEventRepository
{
    private readonly IMongoCollection<EventDocument> events;

    public MongoEventRepository(IMongoDatabase database)
    {
        events = database.GetCollection<EventDocument>("events");
    }

    public async Task AddAsync(EventEntity eventEntity, CancellationToken cancellationToken = default)
    {
        var document = new EventDocument
        {
            Id = eventEntity.Id.ToString(),
            Name = eventEntity.Name,
            Category = eventEntity.Category,
            Venue = eventEntity.Venue,
            City = eventEntity.City,
            StartsAt = eventEntity.StartsAt,
            BasePrice = eventEntity.BasePrice,
            Capacity = eventEntity.Capacity,
            Tags = eventEntity.Tags.ToList()
        };

        await events.InsertOneAsync(document, cancellationToken: cancellationToken);
    }

    public async Task<EventEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var document = await events.Find(eventItem => eventItem.Id == id.ToString()).FirstOrDefaultAsync(cancellationToken);
        return document is null
            ? null
            : new EventEntity
            {
                Id = Guid.Parse(document.Id),
                Name = document.Name,
                Category = document.Category,
                Venue = document.Venue,
                City = document.City,
                StartsAt = document.StartsAt,
                BasePrice = document.BasePrice,
                Capacity = document.Capacity,
                Tags = document.Tags
            };
    }

    public async Task<IReadOnlyList<EventEntity>> ListAsync(CancellationToken cancellationToken = default)
    {
        var documents = await events.Find(_ => true).ToListAsync(cancellationToken);
        return documents.Select(document => new EventEntity
        {
            Id = Guid.Parse(document.Id),
            Name = document.Name,
            Category = document.Category,
            Venue = document.Venue,
            City = document.City,
            StartsAt = document.StartsAt,
            BasePrice = document.BasePrice,
            Capacity = document.Capacity,
            Tags = document.Tags
        }).ToArray();
    }
}
