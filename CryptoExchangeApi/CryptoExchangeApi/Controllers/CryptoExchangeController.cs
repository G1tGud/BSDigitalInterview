using CryptoExchangeApi.Models;
using CryptoExchangeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CryptoExchangeApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CryptoExchangeController : ControllerBase
{
    private readonly IMetaExchangeService _exchangeService;
    private readonly ILogger<CryptoExchangeController> _logger;

    public CryptoExchangeController(ILogger<CryptoExchangeController> logger, IMetaExchangeService exchangeService)
    {
        _logger = logger;
        _exchangeService = exchangeService;
    }

    [HttpGet("orders/executionPlan")]
    public IEnumerable<ExecutionResponse> GetOrderPlan([FromQuery] ExecutionRequest request)
    {
        var res = _exchangeService.CalculateExecutionPlan(request);
        return res;
    }
}