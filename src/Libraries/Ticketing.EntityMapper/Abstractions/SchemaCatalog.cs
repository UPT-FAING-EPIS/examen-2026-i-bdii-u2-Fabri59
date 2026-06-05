namespace Ticketing.EntityMapper.Abstractions;

public sealed record SchemaField(string Name, string Type, bool Required, string Description);

public sealed record SchemaEntity(string Name, string CollectionName, string Description, IReadOnlyList<SchemaField> Fields);

public static class SchemaCatalog
{
    public static IReadOnlyList<SchemaEntity> Entities { get; } =
    [
        new SchemaEntity(
            "Event",
            "events",
            "Catálogo de eventos disponibles para la venta.",
            [
                new SchemaField("Id", "string", true, "Identificador del evento."),
                new SchemaField("Slug", "string", true, "Clave amigable para SEO."),
                new SchemaField("Title", "string", true, "Nombre visible del evento."),
                new SchemaField("Category", "enum", true, "Tipo de evento."),
                new SchemaField("StartsAt", "datetimeoffset", true, "Fecha de inicio."),
                new SchemaField("City", "string", true, "Ciudad sede."),
                new SchemaField("AvailableSeats", "int", true, "Asientos disponibles.")
            ]),
        new SchemaEntity(
            "Ticket",
            "tickets",
            "Entrada emitida para un usuario.",
            [
                new SchemaField("Id", "string", true, "Identificador de la entrada."),
                new SchemaField("EventId", "string", true, "Evento asociado."),
                new SchemaField("UserId", "string", true, "Usuario comprador."),
                new SchemaField("SeatLabel", "string", true, "Asiento o zona asignada."),
                new SchemaField("Status", "enum", true, "Estado de la entrada."),
                new SchemaField("BarcodeToken", "string", true, "Token para QR o código de barras.")
            ]),
        new SchemaEntity(
            "AppUser",
            "users",
            "Usuarios del sistema.",
            [
                new SchemaField("Id", "string", true, "Identificador del usuario."),
                new SchemaField("Email", "string", true, "Correo electrónico."),
                new SchemaField("Role", "enum", true, "Rol de acceso.")
            ])
    ];
}
