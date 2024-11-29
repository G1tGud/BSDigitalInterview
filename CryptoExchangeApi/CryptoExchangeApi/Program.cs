using CryptoExchangeApi.Data;
using CryptoExchangeApi.Middleware;
using CryptoExchangeApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ICryptoExchangeRepository, CryptoExchangeRepository>();

//this is scoped only for testing purposes. It prefills the data every time it is called
//in production we would have a service that would handle data loading
builder.Services.AddScoped<ICryptoExchangeDataProvider, CryptoExchangeDataProvider>(); 
builder.Services.AddScoped<IMetaExchangeService, MetaExchangeService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandler>();
app.MapControllers();

app.Run();