
using WebApi.Domain.Service;

namespace WebApi.Service
{
    public class DatabaseSeedingService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DatabaseSeedingService> _logger;

        public DatabaseSeedingService(IServiceProvider serviceProvider, ILogger<DatabaseSeedingService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var rolService = services.GetRequiredService<IRolService>();
                    _logger.LogInformation("Starting Database Seeding Process at {time}", DateTimeOffset.Now);
                     await rolService.seed();
                    _logger.LogInformation("Finished Database Seeding Process at {time}", DateTimeOffset.Now);


                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred during database seeding");

                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
