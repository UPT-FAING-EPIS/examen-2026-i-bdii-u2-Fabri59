using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ticketing.Application.Contracts.Repositories;
using Ticketing.Domain.Entities;

namespace Ticketing.Infrastructure.Mongo;

public static class DependencyInjection
{
    public static IServiceCollection AddTicketingInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["Ticketing:MongoConnectionString"];
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            services.AddSingleton<IEventRepository, InMemoryEventRepository>();
            services.AddSingleton<ITicketRepository, InMemoryTicketRepository>();
            services.AddSingleton<IUserRepository, InMemoryUserRepository>();
            return services;
        }

        services.AddSingleton<IEventRepository>(_ => new InMemoryEventRepository());
        services.AddSingleton<ITicketRepository>(_ => new InMemoryTicketRepository());
        services.AddSingleton<IUserRepository>(_ => new InMemoryUserRepository());
        return services;
    }
}

internal sealed class InMemoryEventRepository : IEventRepository
{
    private readonly List<EventEntity> items = [];

    public Task AddAsync(EventEntity eventEntity, CancellationToken cancellationToken = default)
    {
        items.Add(eventEntity);
        return Task.CompletedTask;
    }

    public Task<EventEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => Task.FromResult(items.SingleOrDefault(item => item.Id == id));

    public Task<IReadOnlyList<EventEntity>> ListAsync(CancellationToken cancellationToken = default)
        => Task.FromResult((IReadOnlyList<EventEntity>)items.ToArray());
}

internal sealed class InMemoryTicketRepository : ITicketRepository
{
    private readonly List<TicketEntity> items = [];

    public Task AddAsync(TicketEntity ticketEntity, CancellationToken cancellationToken = default)
    {
        items.Add(ticketEntity);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<TicketEntity>> ListByUserAsync(Guid userId, CancellationToken cancellationToken = default)
        => Task.FromResult((IReadOnlyList<TicketEntity>)items.Where(item => item.UserId == userId).ToArray());
}

internal sealed class InMemoryUserRepository : IUserRepository
{
    private readonly List<UserAccountEntity> items = [];

    public Task AddAsync(UserAccountEntity userAccount, CancellationToken cancellationToken = default)
    {
        items.Add(userAccount);
        return Task.CompletedTask;
    }

    public Task<UserAccountEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => Task.FromResult(items.SingleOrDefault(item => item.Id == id));
}
