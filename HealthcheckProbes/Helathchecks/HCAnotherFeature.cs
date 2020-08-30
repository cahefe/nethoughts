using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

public class HCAnotherFeature : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
    {
        var healthCheckResultHealthy = false;

        if (healthCheckResultHealthy)
            return Task.FromResult(HealthCheckResult.Healthy("A healthy result."));

        return Task.FromResult(HealthCheckResult.Unhealthy("An unhealthy result."));
    }
}