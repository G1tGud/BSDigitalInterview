using CryptoExchangeApi.Models;

namespace CryptoExchangeApi.Services;

public interface IMetaExchangeService
{
    public List<ExecutionResponse> CalculateExecutionPlan(ExecutionRequest request);
}