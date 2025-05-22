using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using CurrencyConverter.Infrastructure.Data;
using CurrencyConverter.Core.Interfaces;
using CurrencyConverter.Infrastructure.Services;

class Program
{
    static async Task Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<AppDbContext>(opt =>
                    opt.UseNpgsql("Host=localhost;Port=5432;Database=currency_db;Username=postgres;Password=yourpassword"));

                services.AddHttpClient();
                services.AddScoped<IExchangeService, ExchangeService>();
            }).Build();

        var service = host.Services.GetRequiredService<IExchangeService>();

        Console.WriteLine("Enter amount, from, to:");
        var amount = decimal.Parse(Console.ReadLine()!);
        var from = Console.ReadLine()!;
        var to = Console.ReadLine()!;

        var result = await service.ConvertAsync(amount, from, to);
        Console.WriteLine($"{result.OriginalAmount} {result.From} = {result.ConvertedAmount} {result.To} (Rate: {result.Rate})");
    }
}
