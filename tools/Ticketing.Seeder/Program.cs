using Ticketing.EntityMapper.Migrations;

var runner = new MigrationRunner(
[
    new SampleCatalogMigration()
]);

await runner.RunAsync();
Console.WriteLine("Initial data migration completed.");

sealed class SampleCatalogMigration : IMigrationStep
{
    public string Name => "seed-sample-catalog";
    public int Order => 1;

    public Task ApplyAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Seeding sample events, venues, users and tickets...");
        return Task.CompletedTask;
    }
}
