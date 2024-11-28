using CryptoExchangeApi.Middleware;
using CryptoExchangeApi.Services;

var builder = WebApplication.CreateBuilder(args);
/*
// Add CORS policy
builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        }
    );
});
*/

// Add services to the container.
builder.Services.AddHostedService<DataPreloadingService>();
builder.Services.AddSingleton<IMetaExchangeService, MetaExchangeService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Use CORS
//app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandler>();
app.MapControllers();

app.Run();