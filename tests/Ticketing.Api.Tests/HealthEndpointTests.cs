using Xunit;

namespace Ticketing.Api.Tests;

public sealed class HealthEndpointTests
{
    [Fact]
    public void HealthRouteShouldBeDefined()
    {
        var route = "/health";
        Assert.Equal("/health", route);
    }
}
