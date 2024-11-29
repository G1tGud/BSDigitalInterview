using CryptoExchangeApi.Data;
using CryptoExchangeApi.Mappers;
using CryptoExchangeApi.Models;

namespace CryptoExchangeApi.Services;

public class MetaExchangeService : IMetaExchangeService
{
    private readonly ICryptoExchangeDataProvider _cryptoExchangeDataProvider;
    private readonly ILogger<MetaExchangeService> _logger;

    public MetaExchangeService(ICryptoExchangeDataProvider cryptoExchangeDataProvider, ILoggerFactory loggerFactory)
    {
        _cryptoExchangeDataProvider = cryptoExchangeDataProvider;
        _logger = loggerFactory.CreateLogger<MetaExchangeService>();
    }
    
    
    
    public List<ExecutionResponse> CalculateExecutionPlan(ExecutionRequest request)
    {
        if (request.Amount <= 0)
        {
            throw new ArgumentException("Invalid request amount. Amount must be greater than 0.", nameof(request.Amount));
        }
        
        var ordersToExecute = request.OrderType switch
        {
            "buy" => GetBestExecutionPlan(request, _cryptoExchangeDataProvider.GetAskBookOrders(), _cryptoExchangeDataProvider.GetCryptoExchangeBalanceBtc()),
            "sell" => GetBestExecutionPlan(request, _cryptoExchangeDataProvider.GetBidBookOrders(), _cryptoExchangeDataProvider.GetCryptoExchangeBalanceEur()),
            _ => throw new ArgumentException("Unknown order type", nameof(request.OrderType))
        };

        return ordersToExecute;
    }


    public List<ExecutionResponse> GetBestExecutionPlan(ExecutionRequest request, PriorityQueue<Order, decimal> sortedBookOrders, Dictionary<long, decimal> cryptoExchangeBalance)
    {
        var ordersToExecute = new List<ExecutionResponse>();
        var requestedAmount = request.Amount;
        while (requestedAmount > 0)
        {
            if (sortedBookOrders.Count == 0)
            {
                _logger.LogError("Book order is empty.");
                return null;
            }

            if (cryptoExchangeBalance.Count == 0)
            {
                _logger.LogError("Crypto exchanges are out of balance.");
                return null;
            }
            
            var bestOrder = sortedBookOrders.Dequeue();
            if (!bestOrder.ExchangeId.HasValue)
            {
                _logger.LogError("Missing exchange {Id}.", bestOrder.ExchangeId);
                continue;
            }

            var amountToExecute = Math.Min(requestedAmount, bestOrder.Amount);
            if (!cryptoExchangeBalance.TryGetValue(bestOrder.ExchangeId.Value, out var exchangeBalance))
            {
                //_logger.LogError($"Error: Missing exchange balance for exchange with id {bestOrder.ExchangeId}.");
                continue;
            }
            
            //exchange has no more balance
            if (exchangeBalance == 0)
            {
                _logger.LogError("Error: Crypto exchange with {Id} is out of balance.", bestOrder.ExchangeId);
                cryptoExchangeBalance.Remove(bestOrder.ExchangeId.Value);
                continue;
            }

            // we can only buy as much as exchange has balance
            amountToExecute = Math.Min(amountToExecute, exchangeBalance); 
            
            //lower balance
            cryptoExchangeBalance[bestOrder.ExchangeId.Value] -= amountToExecute;
            requestedAmount -= amountToExecute;
            
            //add order to execute
            var res = new ExecutionResponse
            {
                ExchangeId = bestOrder.ExchangeId.Value,
                OrderType = request.OrderType,
                Amount = amountToExecute,
                Price = bestOrder.Price
            };
            ordersToExecute.Add(res);
        }

        return ordersToExecute;
    }
    
}