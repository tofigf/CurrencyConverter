using CurrencyConverter.Core.Interfaces;
using CurrencyConverter.Grpc;
using Grpc.Core;

namespace CurrencyConverter.GrpcService.Services
{
    public class ExchangeGrpcService : ExchangeConverter.ExchangeConverterBase
    {
        private readonly IExchangeService _exchangeService;

        public ExchangeGrpcService(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }

        public override async Task<ConvertReply> Convert(ConvertRequest request, ServerCallContext context)
        {
            var result = await _exchangeService.ConvertAsync((decimal)request.Amount, request.From, request.To);
            return new ConvertReply
            {
                ConvertedAmount = (double)result.ConvertedAmount,
                Rate = (double)result.Rate,
                Timestamp = result.Timestamp.ToString("O")
            };
        }
    }

}
