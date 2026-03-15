using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MusicXD.Infrastructure.Jobs;

public class SpotifySyncJob : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<SpotifySyncJob> _logger;

    public SpotifySyncJob(IServiceScopeFactory scopeFactory, ILogger<SpotifySyncJob> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Spotify sync job running at {Time}", DateTimeOffset.UtcNow);

            try
            {
                using var scope = _scopeFactory.CreateScope();
                // Sync logic goes here - using scoped services via scope.ServiceProvider
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during Spotify sync");
            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
}
