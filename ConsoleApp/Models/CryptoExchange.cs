namespace ConsoleApp.Models;

public class CryptoExchange
{
    public long ExchangeId { get; set; }
    
    public CryptoExchangeBalance Balance { get; set; }
    
    public BookOrder BookOrder { get; set; }
}