using CryptoExchangeApi.Models;

namespace CryptoExchangeApi.Data;

public interface ICryptoExchangeRepository
{
    public List<CryptoExchange> GetCryptoExchangeData();
}