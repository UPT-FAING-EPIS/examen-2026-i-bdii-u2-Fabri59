namespace Ticketing.EntityMapper.Migrations;

public sealed class MigrationRunner
{
    private readonly IReadOnlyList<IMigrationStep> steps;

    public MigrationRunner(IEnumerable<IMigrationStep> steps)
    {
        this.steps = steps.OrderBy(step => step.Order).ToArray();
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        foreach (var step in steps)
        {
            await step.ApplyAsync(cancellationToken);
        }
    }
}
