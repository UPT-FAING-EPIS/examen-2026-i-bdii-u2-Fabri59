using Ticketing.Domain.Entities;

namespace Ticketing.Application.Contracts.Repositories;

public interface IUserRepository
{
    Task<UserAccountEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(UserAccountEntity userAccount, CancellationToken cancellationToken = default);
}
