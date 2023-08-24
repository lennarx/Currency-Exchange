using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Text.Json;
using TechTalk.SpecFlow;
using VirtualMind.Exchange.API.Controllers;
using VirtualMind.Exchange.API.Exceptions;
using VirtualMind.Exchange.Application.Services.Contracts;
using VirtualMind.Exchange.Application.Services.Contracts.External.Clients;
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
        private ICurrencyService _currencyService;
        private Mock<IExternalHttpClient> _mockExternalHttpClient = new ();
        private Mock<ICurrencyPurchaseRepository> _mockCurrencyPurchaseRepository = new();
        private CurrencyController _currencyController;

        private string _isoCode;
        private IActionResult _currencyExchangeRateQueryResult;
        private Exception _exceptionResult;

        private CurrencyExchangeRate _currencyExchangeRateObjectResult;
        private string[] _curencyExchangeRateStringResults = { "375", "350", "testUpdate" };

        [BeforeScenario]
        public void BeforeSecenario()
        {
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var loggerCurrencyApiService = loggerFactory.CreateLogger<CurrencyExchangeRateApiService>();
            var loggerCurrencyService = loggerFactory.CreateLogger<CurrencyService>();

            _currencyApiService = new CurrencyExchangeRateApiService(Options.Create(TestHelper.GetMockedSettings()), loggerCurrencyApiService, _mockExternalHttpClient.Object);

            _currencyService = new CurrencyService(_currencyApiService, loggerCurrencyService, _mockCurrencyPurchaseRepository.Object);

            _currencyController = new CurrencyController(_currencyService);

            _currencyExchangeRateObjectResult = new CurrencyExchangeRate
            {
                 ISOCode = ISOCode.USD.GetDescriptionFromValue(), PurchaseExchangeRate = 375, SaleExchangeRate = 350 
            };
        }

        [Given(@"a valid '(.*)'")]
        public async Task GivenAValid(string isoCode)
        {
            await Task.Run(() => {
                _isoCode = isoCode;
                var mockedHttpResponseMessage = new HttpResponseMessage
                {
                    Content = new StringContent(JsonSerializer.Serialize(_curencyExchangeRateStringResults)),
                    StatusCode = System.Net.HttpStatusCode.OK
                };
                _mockExternalHttpClient.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(mockedHttpResponseMessage);
            });            
        }

        [Given(@"an invalid isoCode (.*)")]
        public void GivenAnInvalidIsoCode(string isoCode)
        {
            _isoCode = isoCode;
        }

        [Given(@"the external service returns a failure message")]
        public void GivenTheExternalServiceReturnsAFailureMessage()
        {
            var mockedHttpResponseMessage = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                RequestMessage = new HttpRequestMessage { Method = new HttpMethod("GET") },

            };
            _mockExternalHttpClient.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(mockedHttpResponseMessage);
        }

        [Given(@"the external service returns a null response")]
        public void GivenTheExternalServiceReturnsANullResponse()
        {
            var mockedHttpResponseMessage = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = null
            };
            _mockExternalHttpClient.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(mockedHttpResponseMessage);
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

            var expectedResult = _isoCode == ISOCode.USD.GetDescriptionFromValue() ? _currencyExchangeRateObjectResult.PurchaseExchangeRate : _currencyExchangeRateObjectResult.PurchaseExchangeRate / 4;
            resultValue.PurchaseExchangeRate.Should().Be(expectedResult);
        }

        [Then(@"I should see an error")]
        public void ThenIShouldSeeAnError()
        {
            _exceptionResult.Should().NotBeNull();
        }

        [Then(@"the error should include the message (.*)")]
        public void ThenTheErrorShouldIncludeTheMessage(string message)
        {
            _exceptionResult.Message.Should().ContainEquivalentOf(message);
        }
    }
}
