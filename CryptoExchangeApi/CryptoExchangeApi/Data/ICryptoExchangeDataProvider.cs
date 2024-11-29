using CryptoExchangeApi.Models;

namespace CryptoExchangeApi.Data;

public interface ICryptoExchangeDataProvider
{
    public Dictionary<long, decimal> GetCryptoExchangeBalanceEur();
    public Dictionary<long, decimal> GetCryptoExchangeBalanceBtc();
    public PriorityQueue<Order, decimal> GetAskBookOrders();
    public PriorityQueue<Order, decimal> GetBidBookOrders();
}