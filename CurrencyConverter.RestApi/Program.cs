using CurrencyConverter.Core.Interfaces;
using CurrencyConverter.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<CurrencyConverter.Infrastructure.Data.AppDbContext>(options =>
   options.UseNpgsql(connectionString));

builder.Services.AddScoped<IExchangeService, ExchangeService>();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddGrpc();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
