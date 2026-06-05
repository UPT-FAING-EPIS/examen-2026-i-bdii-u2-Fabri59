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
        var document = EventDocument.FromEntity(eventEntity);

        await events.InsertOneAsync(document, cancellationToken: cancellationToken);
    }

    public async Task<EventEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var document = await events.Find(eventItem => eventItem.Id == id).FirstOrDefaultAsync(cancellationToken);
        return document?.ToEntity();
    }

    public async Task<IReadOnlyList<EventEntity>> ListAsync(CancellationToken cancellationToken = default)
    {
        var documents = await events.Find(_ => true).ToListAsync(cancellationToken);
        return documents.Select(document => document.ToEntity()).ToArray();
    }
}
