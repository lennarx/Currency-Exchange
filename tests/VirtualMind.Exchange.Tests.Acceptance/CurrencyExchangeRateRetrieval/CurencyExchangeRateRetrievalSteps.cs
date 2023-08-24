using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Text.Json;
using TechTalk.SpecFlow;
using VirtualMind.Exchange.API.Controllers;
using VirtualMind.Exchange.Application.Services.Contracts;
using VirtualMind.Exchange.Application.Services.Implementations;
using VirtualMind.Exchange.Application.Services.Implementations.External;
using VirtualMind.Exchange.Application.Services.Utils.Extensions;
using VirtualMind.Exchange.Domain;
using VirtualMind.Exchange.Domain.Domain.Contracts.External;
using VirtualMind.Exchange.Infrastructure.Entities.Enums;
using VirtualMind.Exchange.Infrastructure.Repositories.Currency.Contracts;
using Xunit;

namespace VirtualMind.Exchange.Tests.Acceptance.CurrencyExchangeRateRetrieval
{
    [Binding]
    public class CurencyExchangeRateRetrievalSteps
    {
        private ICurrencyExchangeRateApiService _currencyApiService;
        private Mock<HttpClient> _mockedClient = new();
        private ICurrencyService _currencyService;
        private Mock<ICurrencyPurchaseRepository> _mockCurrencyPurchaseRepository = new();
        private CurrencyController _currencyController;

        private string _isoCode;
        private IActionResult _currencyExchangeRateQueryResult;
        private Exception _exceptionResult;

        private IList<CurrencyExchangeRate> _currencyExchangeRateObjectResults;

        [BeforeScenario]
        public void BeforeSecenario()
        {
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var loggerCurrencyApiService = loggerFactory.CreateLogger<CurrencyExchangeRateApiService>();
            var loggerCurrencyService = loggerFactory.CreateLogger<CurrencyService>();

            _currencyApiService = new CurrencyExchangeRateApiService(Options.Create(TestHelper.GetMockedSettings()), loggerCurrencyApiService, _mockedClient.Object);

            _currencyService = new CurrencyService(_currencyApiService, loggerCurrencyService, _mockCurrencyPurchaseRepository.Object);

            _currencyController = new CurrencyController(_currencyService);

            _currencyExchangeRateObjectResults = new List<CurrencyExchangeRate>
            {
                { new CurrencyExchangeRate{ ISOCode = ISOCode.USD.GetDescriptionFromValue(), PurchaseExchangeRate = 375, SaleExchangeRate = 350 } },
                { new CurrencyExchangeRate{ ISOCode = ISOCode.BRL.GetDescriptionFromValue(), PurchaseExchangeRate = 93.75, SaleExchangeRate = 87.5 } }
            };
        }

        [Given(@"a valid '(.*)'")]
        public void GivenAValid(string isoCode)
        {
            _isoCode = isoCode;
            var mockedResultToReturn = _currencyExchangeRateObjectResults.First(x => x.ISOCode.Equals(isoCode));
            var mockedHttpResponseMessage = new HttpResponseMessage
            {
                Content = new StringContent(JsonSerializer.Serialize(mockedResultToReturn)),
                StatusCode = System.Net.HttpStatusCode.OK
            };
            _mockedClient.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(mockedHttpResponseMessage);
        }

        [When(@"I try to retrieve the currency exchange rate")]
        public async Task WhenITryToRetrieveTheCurrencyExchangeRate()
        {
            try
            {
                _currencyExchangeRateQueryResult = await _currencyController.GetCurrencyExchangeRate(_isoCode);
            }
            catch (Exception ex)
            {
                _exceptionResult = ex;
            }
        }

        [Then(@"I should see the exchange rate returned")]
        public void ThenIShouldSeeTheExchangeRateReturned()
        {
            var okResult = Assert.IsType<OkObjectResult>(_currencyExchangeRateQueryResult);
            var resultValue = okResult.Value as CurrencyExchangeRate;

            resultValue.PurchaseExchangeRate.Should().Be(_currencyExchangeRateObjectResults.First(x => x.ISOCode.Equals(_isoCode)).PurchaseExchangeRate);
        }
    }
}
