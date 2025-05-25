using CurrencyConverter.Core.Interfaces;
using CurrencyConverter.GrpcService.Services;
using CurrencyConverter.Infrastructure.Data;
using CurrencyConverter.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpClient(); 
builder.Services.AddScoped<IExchangeService, ExchangeService>();
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<ExchangeGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.Run();
