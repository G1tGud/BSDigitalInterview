using CryptoExchangeApi.Models;

namespace CryptoExchangeApi.Services;

public interface IMetaExchangeService
{
    Task PreloadDataAsync();
    public List<ExecutionResponse> CalculateExecutionPlan(ExecutionRequest request);
}