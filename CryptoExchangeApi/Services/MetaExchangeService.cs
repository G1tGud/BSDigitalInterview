using CryptoExchangeApi.Data;
using CryptoExchangeApi.Mappers;
using CryptoExchangeApi.Models;

namespace CryptoExchangeApi.Services;

public class MetaExchangeService : IMetaExchangeService
{
    private Dictionary<long, decimal> _cryptoExchangeBalanceEur;
    private Dictionary<long, decimal> _cryptoExchangeBalanceBtc;
    private PriorityQueue<Order, decimal> _askBookOrders;
    private PriorityQueue<Order, decimal> _bidBookOrders;
    private readonly ILogger<MetaExchangeService> _logger;

    public MetaExchangeService(ILogger<MetaExchangeService> logger)
    {
        _logger = logger;
    }

    public Task PreloadDataAsync()
    {
        var exchanges = CryptoExchangeData.GetCryptoExchangeData();
        _cryptoExchangeBalanceEur = exchanges.ToDictionary(x => x.ExchangeId, x => x.Balance.Eur);
        _cryptoExchangeBalanceBtc = exchanges.ToDictionary(x => x.ExchangeId, x => x.Balance.Btc);
        
        var askOrder = BookOrderMapper.GetAskOrdersWithExchangeId(exchanges).ToList();
        _askBookOrders = GetSortedOrders(askOrder, "asc");
        
        var bidOrder = BookOrderMapper.GetBidOrdersWithExchangeId(exchanges).ToList();
        _bidBookOrders = GetSortedOrders(bidOrder, "desc");
        
        return Task.CompletedTask;
    }
    
    
    public List<ExecutionResponse> CalculateExecutionPlan(ExecutionRequest request)
    {
        if (request.Amount <= 0)
        {
            throw new ArgumentException("Invalid request amount. Amount must be greater than 0.", nameof(request.Amount));
        }
        
        var ordersToExecute = request.OrderType switch
        {
            "buy" => GetBestExecutionPlan(request.Amount, _askBookOrders, _cryptoExchangeBalanceBtc),
            "sell" => GetBestExecutionPlan(request.Amount, _bidBookOrders, _cryptoExchangeBalanceEur),
            _ => throw new ArgumentException("Unknown order type", nameof(request.OrderType))
        };

        return ordersToExecute;
    }


    public List<ExecutionResponse> GetBestExecutionPlan(decimal requestedAmount, PriorityQueue<Order, decimal> sortedBookOrders, Dictionary<long, decimal> cryptoExchangeBalance)
    {
        var ordersToExecute = new List<ExecutionResponse>();
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
                OrderType = bestOrder.Type,
                Amount = amountToExecute,
                Price = bestOrder.Price
            };
            ordersToExecute.Add(res);
        }

        return ordersToExecute;
    }


    public static PriorityQueue<Order, decimal> GetSortedOrders(List<Order> orders, string sortDirection)
    {
        var sortedOrders = sortDirection switch
        {
            "asc" => new PriorityQueue<Order, decimal>(orders.Count),
            "desc" => new PriorityQueue<Order, decimal>(orders.Count, new MaxQueueComparer()),
            _ => throw new ArgumentException("Unsupported sorting direction", nameof(sortDirection))
        };

        foreach (var order in orders)
        {
            sortedOrders.Enqueue(order, order.Price);
        }
        
        return sortedOrders;
    }
    
    public class MaxQueueComparer : IComparer<decimal>
    {
        public int Compare(decimal x, decimal y)
        {
            return y.CompareTo(x);
        }
    }
    
}