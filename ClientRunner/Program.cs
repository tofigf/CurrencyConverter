using CurrencyConverter.Clients.Grpc;
using CurrencyConverter.Clients.Rest;
using CurrencyConverter.Grpc;
using Grpc.Net.Client;

Console.WriteLine("Choose mode: rest or grpc");
var mode = Console.ReadLine()?.Trim().ToLower();

if (mode == "rest")
{
    var httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7263") };
    var restClient = new RestExchangeClient(httpClient);

    var result = await restClient.ConvertAsync(100, "USD", "EUR");
    Console.WriteLine($"{result.OriginalAmount} {result.From} = {result.ConvertedAmount} {result.To} (Rate: {result.Rate})");
}
else if (mode == "grpc")
{
    var channel = GrpcChannel.ForAddress("https://localhost:7194");
    var grpcClient = new GrpcExchangeClient(new ExchangeConverter.ExchangeConverterClient(channel));

    var result = await grpcClient.ConvertAsync(100, "USD", "EUR");
    Console.WriteLine($"{result.OriginalAmount} {result.From} = {result.ConvertedAmount} {result.To} (Rate: {result.Rate})");
}
else
{
    Console.WriteLine("Invalid mode. Use 'rest' or 'grpc'.");
}

Console.WriteLine("Press Enter to exit...");
Console.ReadLine();
