using System.ComponentModel.DataAnnotations;

namespace CryptoExchangeApi.Models;

public class ExecutionRequest
{
    [Required]
    public string OrderType { get; set; }
    public decimal Amount { get; set; }
}