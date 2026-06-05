using Ticketing.Domain.Entities;

namespace Ticketing.Application.Contracts.Repositories;

public interface IEventRepository
{
    Task<IReadOnlyList<EventEntity>> ListAsync(CancellationToken cancellationToken = default);
    Task<EventEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(EventEntity eventEntity, CancellationToken cancellationToken = default);
}
