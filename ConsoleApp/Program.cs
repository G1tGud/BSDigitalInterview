using System.Text.Json;
using ConsoleApp;
using ConsoleApp.Models;
using ConsoleApp.Services;

//read data
var bookOrders = BookOrderService.GetBookOrders();
var request = new ExecutionRequest
{
    OrderType = "sell",
    Amount = 20
};


//calculate best orders
var result = MetaExchangeService.CalculateExecutionPlan(request, bookOrders);


//print
Console.WriteLine(JsonSerializer.Serialize(result));