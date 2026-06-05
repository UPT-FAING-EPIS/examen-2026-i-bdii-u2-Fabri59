using Ticketing.Application.Contracts.Repositories;
using Ticketing.Domain.Entities;

namespace Ticketing.Application.Services;

public sealed class EventCatalogService
{
    private readonly IEventRepository eventRepository;

    public EventCatalogService(IEventRepository eventRepository)
    {
        this.eventRepository = eventRepository;
    }

    public Task<IReadOnlyList<EventEntity>> ListEventsAsync(CancellationToken cancellationToken = default)
        => eventRepository.ListAsync(cancellationToken);

    public Task<EventEntity?> GetEventAsync(Guid id, CancellationToken cancellationToken = default)
        => eventRepository.GetByIdAsync(id, cancellationToken);
}
