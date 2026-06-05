using Ticketing.EntityMapper.Abstractions;

var outputRoot = args.FirstOrDefault() ?? "docs/database";
Directory.CreateDirectory(outputRoot);

var markdownPath = Path.Combine(outputRoot, "entities.md");
var diagramPath = Path.Combine(outputRoot, "schema.mmd");

await File.WriteAllTextAsync(markdownPath, BuildMarkdown());
await File.WriteAllTextAsync(diagramPath, BuildDiagram());

Console.WriteLine($"Generated {markdownPath}");
Console.WriteLine($"Generated {diagramPath}");

static string BuildMarkdown()
{
    var builder = new System.Text.StringBuilder();
    builder.AppendLine("# Entity Catalog");
    builder.AppendLine();

    foreach (var entity in SchemaCatalog.Entities)
    {
        builder.AppendLine($"## {entity.Name}");
        builder.AppendLine($"Collection: `{entity.CollectionName}`");
        builder.AppendLine(entity.Description);
        builder.AppendLine();
        builder.AppendLine("| Field | Type | Required | Description |");
        builder.AppendLine("| --- | --- | --- | --- |");
        foreach (var field in entity.Fields)
        {
            builder.AppendLine($"| {field.Name} | {field.Type} | {(field.Required ? "Yes" : "No")} | {field.Description} |");
        }
        builder.AppendLine();
    }

    return builder.ToString();
}

static string BuildDiagram()
{
    return """
    erDiagram
      EVENTS {
        string Id
        string Slug
        string Title
        string Category
        string City
      }
      TICKETS {
        string Id
        string EventId
        string UserId
        string SeatLabel
      }
      USERS {
        string Id
        string Email
        string Role
      }
      EVENTS ||--o{ TICKETS : has
      USERS ||--o{ TICKETS : buys
    """;
}
