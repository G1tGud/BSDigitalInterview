using System.Text.Json;
using CryptoExchangeApi.Mappers;
using CryptoExchangeApi.Models;
using CryptoExchangeApi.Utils;

namespace CryptoExchangeApi.Data;

public class CryptoExchangeDataProvider : ICryptoExchangeDataProvider
{
    private Dictionary<long, decimal> _cryptoExchangeBalanceEur;
    private Dictionary<long, decimal> _cryptoExchangeBalanceBtc;
    private PriorityQueue<Order, decimal> _askBookOrders;
    private PriorityQueue<Order, decimal> _bidBookOrders;
    
    private readonly ICryptoExchangeRepository _cryptoExchangeRepository;

    public CryptoExchangeDataProvider(ICryptoExchangeRepository cryptoExchangeRepository)
    {
        _cryptoExchangeRepository = cryptoExchangeRepository;
        PreloadDataAsync();
    }


    public Dictionary<long, decimal> GetCryptoExchangeBalanceEur()
    {
        return _cryptoExchangeBalanceEur;
    }
    public Dictionary<long, decimal> GetCryptoExchangeBalanceBtc()
    {
        return _cryptoExchangeBalanceBtc;
    }
    public PriorityQueue<Order, decimal> GetAskBookOrders()
    {
        return _askBookOrders;
    }
    public PriorityQueue<Order, decimal> GetBidBookOrders()
    {
        return _bidBookOrders;
    }
    
    
    public Task PreloadDataAsync()
    {
        var exchanges = _cryptoExchangeRepository.GetCryptoExchangeData();
        
        _cryptoExchangeBalanceEur = exchanges.ToDictionary(x => x.ExchangeId, x => x.Balance.Eur);
        _cryptoExchangeBalanceBtc = exchanges.ToDictionary(x => x.ExchangeId, x => x.Balance.Btc);
        
        var askOrder = BookOrderMapper.GetAskOrdersWithExchangeId(exchanges).ToList();
        _askBookOrders = SortingService.GetSortedOrders(askOrder, "asc");
        
        var bidOrder = BookOrderMapper.GetBidOrdersWithExchangeId(exchanges).ToList();
        _bidBookOrders = SortingService.GetSortedOrders(bidOrder, "desc");
        
        return Task.CompletedTask;
    }
    
    
    
       
}