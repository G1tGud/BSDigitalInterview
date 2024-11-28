namespace CryptoExchangeApi.Services;

public class DataPreloadingService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DataPreloadingService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var metaExchangeService = scope.ServiceProvider.GetRequiredService<IMetaExchangeService>();
        await metaExchangeService.PreloadDataAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // Clean up if needed
        return Task.CompletedTask;
    }
}