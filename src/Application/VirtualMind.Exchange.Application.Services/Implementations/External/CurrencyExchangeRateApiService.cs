using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;
using VirtualMind.Exchange.Application.Services.Implementations.External.Responses;
using VirtualMind.Exchange.Application.Services.Settings.External;
using VirtualMind.Exchange.Domain.Domain.Contracts.External;
using VirtualMind.Exchange.Infrastructure.Entities.Enums;

namespace VirtualMind.Exchange.Application.Services.Implementations.External
{
    public class CurrencyExchangeRateApiService : ICurrencyExchangeRateApiService
    {
        private readonly ExternalCurrencyExchangeRateServiceSettings _externalCurrencyExchangeRateServiceSettings;
        private readonly ILogger<CurrencyExchangeRateApiService> _logger;

        public CurrencyExchangeRateApiService(IOptions<ExternalCurrencyExchangeRateServiceSettings> externalCurrencyExchangeRateServiceSettings, ILogger<CurrencyExchangeRateApiService> logger)
        {
            _externalCurrencyExchangeRateServiceSettings = externalCurrencyExchangeRateServiceSettings.Value;
            _logger = logger;
        }
        public async Task<CurrencyExchangeRateApiResponse> RetrieveCurrencyExchangeRateAsync(ISOCode isoCode)
        {
            var url = _externalCurrencyExchangeRateServiceSettings.ExternalServicesUrls.First(x => x.ISOCode == isoCode).Url;
            var client = new HttpClient();
            _logger.LogInformation($"Requesting exchange rate for currency {isoCode}");
            var externalServiceResponse = await client.GetAsync(url);

            try
            { 
                externalServiceResponse.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                _logger.LogError($"An error occurred while attempting to retrieve the currency from the external service. The error code was {externalServiceResponse.StatusCode}" +
                    $"and the reason was {externalServiceResponse.ReasonPhrase}");
                throw new Exception($"Request fail: Method: {externalServiceResponse.RequestMessage.Method.Method}, uri : {externalServiceResponse.RequestMessage.RequestUri.AbsoluteUri}");
            }

            var stringResponse = await externalServiceResponse.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(stringResponse))
            {
                return null;
            }

            var deserializedStringExchangeRateResponse = JsonSerializer.Deserialize<List<string>>(stringResponse);

            return new CurrencyExchangeRateApiResponse
            {
                PurchaseExchangeRate = Convert.ToDouble(deserializedStringExchangeRateResponse[0]),
                SaleExchangeRate = Convert.ToDouble(deserializedStringExchangeRateResponse[1]),
                LastUpdate = deserializedStringExchangeRateResponse[2]
            };
        }
    }
}
