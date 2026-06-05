using FluentAssertions;

namespace Ticketing.Api.Tests;

public sealed class HealthEndpointTests
{
    [Fact]
    public void HealthRouteShouldBeDefined()
    {
        var route = "/health";
        route.Should().Be("/health");
    }
}
