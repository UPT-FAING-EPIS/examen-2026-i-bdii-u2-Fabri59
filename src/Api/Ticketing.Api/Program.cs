using Ticketing.Application.Contracts.Repositories;
using Ticketing.Application.Events;
using Ticketing.Application.Reports;
using Ticketing.Application.Security;
using Ticketing.Application.Services;
using Ticketing.Application.Tickets;
using Ticketing.Domain.Entities;
using Ticketing.Infrastructure.Mongo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTicketingInfrastructure(builder.Configuration);
builder.Services.AddSingleton<EventCatalogService>();
builder.Services.AddSingleton<TicketOperationsService>();
builder.Services.AddSingleton<ReportService>();
builder.Services.AddSingleton<AuthService>();

var app = builder.Build();

app.UseHttpsRedirection();

await EnsureSampleDataAsync(app.Services);

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.MapGet("/events", async (string? query, string? city, IEventRepository eventRepository, CancellationToken cancellationToken) =>
{
    var events = await eventRepository.ListAsync(cancellationToken);
    var filtered = events.Where(item =>
        string.IsNullOrWhiteSpace(query) || item.Name.Contains(query, StringComparison.OrdinalIgnoreCase) || item.City.Contains(query, StringComparison.OrdinalIgnoreCase))
        .Where(item => string.IsNullOrWhiteSpace(city) || item.City.Contains(city, StringComparison.OrdinalIgnoreCase))
        .Select(item => new EventSummaryDto(
            item.Id.ToString(),
            item.Name.ToLowerInvariant().Replace(' ', '-'),
            item.Name,
            Ticketing.Domain.Events.EventCategory.Concert,
            item.StartsAt,
            item.City,
            item.BasePrice,
            item.Capacity))
        .ToArray();

    return Results.Ok(filtered);
});

app.MapGet("/events/{id:guid}", async (Guid id, EventCatalogService catalogService, CancellationToken cancellationToken) =>
{
    var eventEntity = await catalogService.GetEventAsync(id, cancellationToken);
    return eventEntity is null ? Results.NotFound() : Results.Ok(new EventDetailDto(
        eventEntity.Id.ToString(),
        eventEntity.Id.ToString("N"),
        eventEntity.Name,
        $"Detalles del evento {eventEntity.Name}",
        Ticketing.Domain.Events.EventCategory.Concert,
        eventEntity.StartsAt,
        eventEntity.Venue,
        eventEntity.City,
        eventEntity.BasePrice,
        eventEntity.Capacity,
        eventEntity.Capacity,
        eventEntity.Tags.ToList()));
});

app.MapPost("/events", async (CreateEventRequest request, IEventRepository repository, CancellationToken cancellationToken) =>
{
    var eventEntity = new EventEntity
    {
        Name = request.Title,
        Category = request.Category.ToString(),
        Venue = request.VenueId,
        City = request.City,
        StartsAt = request.StartsAt,
        BasePrice = request.BasePrice,
        Capacity = request.Capacity,
        Tags = request.Artists
    };

    await repository.AddAsync(eventEntity, cancellationToken);
    return Results.Created($"/events/{eventEntity.Id}", new { eventEntity.Id });
});

app.MapPost("/auth/register", (RegisterRequest request, AuthService authService) => Results.Ok(authService.Register(request)));
app.MapPost("/auth/login", (LoginRequest request, AuthService authService) => Results.Ok(authService.Login(request)));
app.MapPost("/tickets", async (PurchaseTicketRequest request, TicketOperationsService ticketService, CancellationToken cancellationToken) => Results.Ok(await ticketService.PurchaseAsync(request, cancellationToken)));
app.MapGet("/tickets/{userId:guid}", async (Guid userId, ITicketRepository repository, CancellationToken cancellationToken) => Results.Ok(await repository.ListByUserAsync(userId, cancellationToken)));
app.MapGet("/tickets/{id:guid}/download", (Guid id, TicketOperationsService ticketService) => Results.File(ticketService.BuildTicketFile(id), "application/pdf", $"ticket-{id:N}.pdf"));
app.MapPost("/reports", (SalesReportRequest request, ReportService reportService) => Results.Ok(reportService.CreateSalesReport(request)));

app.Run();

static async Task EnsureSampleDataAsync(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var eventRepository = scope.ServiceProvider.GetRequiredService<IEventRepository>();
    var events = await eventRepository.ListAsync();
    if (events.Count > 0)
    {
        return;
    }

    await eventRepository.AddAsync(new EventEntity
    {
        Name = "Festival de Verano",
        Category = "Concert",
        Venue = "Arena Central",
        City = "Madrid",
        StartsAt = DateTimeOffset.UtcNow.AddDays(21),
        BasePrice = 45,
        Capacity = 1500,
        Tags = ["Live", "Festival"]
    });

    await eventRepository.AddAsync(new EventEntity
    {
        Name = "Final de Temporada",
        Category = "Sports",
        Venue = "Estadio Norte",
        City = "Barcelona",
        StartsAt = DateTimeOffset.UtcNow.AddDays(12),
        BasePrice = 25,
        Capacity = 8000,
        Tags = ["Deportes", "Premium"]
    });
}

public partial class Program;
