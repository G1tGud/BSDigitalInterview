using CryptoExchangeApi.Models;

namespace CryptoExchangeApi.Mappers;

public class BookOrderMapper
{

    public static IEnumerable<Order> GetAskOrdersWithExchangeId(List<CryptoExchange> exchanges)
    {
        return exchanges.SelectMany(exchange => exchange.BookOrder.Asks.Select(
            x =>
            {
                x.Order.ExchangeId = exchange.ExchangeId;
                return x.Order;
            })
        );
    }
    
    public static IEnumerable<Order> GetBidOrdersWithExchangeId(List<CryptoExchange> exchanges)
    {
        return exchanges.SelectMany(exchange => exchange.BookOrder.Bids.Select(
            x =>
            {
                x.Order.ExchangeId = exchange.ExchangeId;
                return x.Order;
            })
        );
    }
    
}