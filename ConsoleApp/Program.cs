using System.Text.Json;
using ConsoleApp.Data;
using ConsoleApp.Models;
using ConsoleApp.Services;

//read data
var cryptoExchanges = CryptoExchangeData.GetCryptoExchangeData();
var request = new ExecutionRequest
{
    OrderType = "sell",
    Amount = 10
};


//calculate best orders
var result = MetaExchangeService.CalculateExecutionPlan(request, cryptoExchanges);


//print
Console.WriteLine(JsonSerializer.Serialize(result));