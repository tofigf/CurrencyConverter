using CurrencyConverter.Core.Interfaces;
using CurrencyConverter.Core.Models;
using CurrencyConverter.RestApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.RestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        private readonly IExchangeService _exchangeService;
        public ConvertController(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }

        [HttpPost]
        public async Task<ActionResult<ExchangeResult>> Convert([FromBody] ConvertRequest request)
        {
            var result = await _exchangeService.ConvertAsync(request.Amount, request.From, request.To);
            return Ok(result);
        }
    }
}
