using System.Net;
using System.Text.Json;

namespace CurrencyConverter.Tests
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly JsonElement _mockResponse;

        public MockHttpMessageHandler(JsonElement mockResponse)
        {
            _mockResponse = mockResponse;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(_mockResponse.ToString())
            };
            return Task.FromResult(response);
        }
    }
}
