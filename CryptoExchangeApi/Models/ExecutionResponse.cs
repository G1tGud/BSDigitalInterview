namespace CryptoExchangeApi.Models;

public class ExecutionResponse
{
    public long ExchangeIndex { get; set; }
    public string OrderType { get; set; }
    public decimal Amount { get; set; }
    public decimal Price { get; set; }
}