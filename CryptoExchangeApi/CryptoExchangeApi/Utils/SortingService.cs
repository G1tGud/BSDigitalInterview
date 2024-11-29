using CryptoExchangeApi.Models;

namespace CryptoExchangeApi.Utils;

public class SortingService
{
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