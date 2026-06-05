namespace Ticketing.EntityMapper.Migrations;

public interface IMigrationStep
{
    string Name { get; }
    int Order { get; }
    Task ApplyAsync(CancellationToken cancellationToken = default);
}
