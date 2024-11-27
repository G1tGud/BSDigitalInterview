using ConsoleApp.Mappers;
using ConsoleApp.Models;

namespace ConsoleApp.Services;

public class MetaExchangeService
{

    public static List<ExecutionResponse> CalculateExecutionPlan(ExecutionRequest request, List<BookOrder> bookOrders)
    {
        //sort book orders
        PriorityQueue<Order, decimal> sortedBookOrders;
        switch (request.OrderType)
        {
            case "buy":
                var askOrder = BookOrderMapper.GetAskOrdersWithExchangeId(bookOrders).ToList();
                sortedBookOrders = GetSortedOrders(askOrder, "asc");
                break;
            case "sell":
                var bidOrder = BookOrderMapper.GetBidOrdersWithExchangeId(bookOrders).ToList();
                sortedBookOrders = GetSortedOrders(bidOrder, "desc");
                break;
            default:
                throw new ArgumentException("Unknown order type", nameof(request.OrderType));
        }

        //get the best execution plan
        var requestedAmount = request.Amount;
        var ordersToExecute = new List<ExecutionResponse>();
        while (sortedBookOrders.Count > 0 && requestedAmount > 0)
        {
            var bestOrder = sortedBookOrders.Dequeue();
            if (!bestOrder.ExchangeIndex.HasValue)
            {
                //log error and continue
                Console.WriteLine($"Missing exchange index order {bestOrder.Id}.");
                continue;
            }

            var amountToExecute = Math.Min(requestedAmount, bestOrder.Amount);
            var res = new ExecutionResponse
            {
                ExchangeIndex = bestOrder.ExchangeIndex.Value,
                OrderType = bestOrder.Type,
                Amount = amountToExecute,
                Price = bestOrder.Price
            };
            ordersToExecute.Add(res);
            

            requestedAmount -= amountToExecute;
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