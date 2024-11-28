using ConsoleApp.Mappers;
using ConsoleApp.Models;

namespace ConsoleApp.Services;

public class MetaExchangeService
{

    public static List<ExecutionResponse> CalculateExecutionPlan(ExecutionRequest request, List<CryptoExchange> exchanges)
    {
        //sort book orders
        PriorityQueue<Order, decimal> sortedBookOrders;
        Dictionary<long, decimal> cryptoExchangeBalance;
        switch (request.OrderType)
        {
            case "buy":
                var askOrder = BookOrderMapper.GetAskOrdersWithExchangeId(exchanges).ToList();
                cryptoExchangeBalance = exchanges.ToDictionary(x => x.ExchangeId, x => x.Balance.Btc);
                sortedBookOrders = GetSortedOrders(askOrder, "asc");
                break;
            case "sell":
                var bidOrder = BookOrderMapper.GetBidOrdersWithExchangeId(exchanges).ToList();
                cryptoExchangeBalance = exchanges.ToDictionary(x => x.ExchangeId, x => x.Balance.Eur);
                sortedBookOrders = GetSortedOrders(bidOrder, "desc");
                break;
            default:
                throw new ArgumentException("Unknown order type", nameof(request.OrderType));
        }

        //get the best execution plan
        var ordersToExecute = GetBestExecutionPlan(request.Amount, sortedBookOrders, cryptoExchangeBalance);

        return ordersToExecute;
    }


    public static List<ExecutionResponse> GetBestExecutionPlan(decimal requestedAmount, PriorityQueue<Order, decimal> sortedBookOrders, 
        Dictionary<long, decimal> cryptoExchangeBalance)
    {
        var ordersToExecute = new List<ExecutionResponse>();
        while (requestedAmount > 0)
        {
            if (sortedBookOrders.Count == 0)
            {
                Console.WriteLine("Error: Book order is empty.");
                return null;
            }

            if (cryptoExchangeBalance.Count == 0)
            {
                Console.WriteLine("Error: Crypto exchanges are out of balance.");
                return null;
            }
            
            
            var bestOrder = sortedBookOrders.Dequeue();
            if (!bestOrder.ExchangeId.HasValue)
            {
                //log error and continue
                Console.WriteLine($"Error: Missing exchange id {bestOrder.ExchangeId}.");
                continue;
            }

            var amountToExecute = Math.Min(requestedAmount, bestOrder.Amount);
            if (!cryptoExchangeBalance.TryGetValue(bestOrder.ExchangeId.Value, out var exchangeBalance))
            {
                //Console.WriteLine($"Error: Missing exchange balance for exchange with id {bestOrder.ExchangeId}.");
                continue;
            }
            
            //exchange has no more balance
            if (exchangeBalance == 0)
            {
                Console.WriteLine($"Error: Crypto exchange with id {bestOrder.ExchangeId} is out of balance.");
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