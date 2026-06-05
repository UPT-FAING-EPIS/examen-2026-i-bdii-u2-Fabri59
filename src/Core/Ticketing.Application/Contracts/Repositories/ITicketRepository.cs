using Ticketing.Domain.Entities;

namespace Ticketing.Application.Contracts.Repositories;

public interface ITicketRepository
{
    Task<IReadOnlyList<TicketEntity>> ListByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(TicketEntity ticketEntity, CancellationToken cancellationToken = default);
}
