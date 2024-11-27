using ConsoleApp.Models;

namespace ConsoleApp.Mappers;

public class BookOrderMapper
{

    public static IEnumerable<Order> GetAskOrdersWithExchangeId(List<BookOrder> bookOrders)
    {
        return bookOrders.SelectMany((bookOrder, index) => bookOrder.Asks.Select(
            x =>
            {
                x.Order.ExchangeIndex = index;
                return x.Order;
            })
        );
    }
    
    public static IEnumerable<Order> GetBidOrdersWithExchangeId(List<BookOrder> bookOrders)
    {
        return bookOrders.SelectMany((bookOrder, index) => bookOrder.Bids.Select(
            x =>
            {
                x.Order.ExchangeIndex = index;
                return x.Order;
            })
        );
    }
    
}