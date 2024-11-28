namespace CryptoExchangeApi.Models;

public class BookOrder
{
    public DateTime AcqTime { get; set; }
    public List<Bid> Bids { get; set; }
    public List<Ask> Asks { get; set; }
}

public class Bid
{
    public Order Order { get; set; }
}

public class Ask
{
    public Order Order { get; set; }
}

public class Order
{
    public long? Id { get; set; }
    public long? ExchangeId { get; set; }
    public DateTime Time { get; set; }
    public string Type { get; set; }
    public string Kind { get; set; }
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
}