using Microsoft.AspNetCore.Mvc;
using Moq;
using TechTalk.SpecFlow;
using VirtualMind.Exchange.API.Controllers;
using VirtualMind.Exchange.Application.Services.Contracts;
using VirtualMind.Exchange.Domain.Domain.Contracts.External;
using VirtualMind.Exchange.Domain;
using VirtualMind.Exchange.Infrastructure.Repositories.Currency.Contracts;
using Microsoft.Extensions.Logging;
using VirtualMind.Exchange.Application.Services.Implementations;
using VirtualMind.Exchange.Infrastructure.Entities.Enums;
using VirtualMind.Exchange.Application.Services.Utils.Extensions;
using VirtualMind.Exchange.Infrastructure.Repositories.Currency.Implementations;
using FluentAssertions;
using Xunit;
using System.Reflection.Metadata;
using VirtualMind.Exchange.Application.Services.Implementations.External.Responses;
using System.Reflection.Metadata.Ecma335;
using VirtualMind.Exchange.Application.Services.Utils.Consts;

namespace VirtualMind.Exchange.Tests.Acceptance.Features.CurrencyPurchaseFeature
{
    [Binding]
    public class CurrencyPurchaseSteps
    {
        private Mock<ICurrencyExchangeRateApiService> _mockCurrencyApiService = new();
        private ICurrencyService _currencyService;
        private ICurrencyPurchaseRepository _currencyPurchaseRepository;
        private CurrencyController _currencyController;

        private string _isoCode;
        private IActionResult _currencyPurchaseResult;
        private Exception _exceptionResult;

        private const double _saleExchangeRate = 375;

        private CurrencyExchangeRate _currencyExchangeRateObjectResult;
        private string[] _curencyExchangeRateStringResults = { "375", "350", "testUpdate" };

        private ulong _userId;
        private ulong _defaultUserIdValue = 1;

        private int _amountToBuyInPesos;

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var loggerCurrencyService = loggerFactory.CreateLogger<CurrencyService>();

            var context = await TestHelper.GetInMemoryCurrencyRepositoryTestAsync();

            _currencyPurchaseRepository = new CurrencyPurchaseRepository(context);

            _currencyService = new CurrencyService(_mockCurrencyApiService.Object, loggerCurrencyService, _currencyPurchaseRepository);

            _currencyController = new CurrencyController(_currencyService);

            _currencyExchangeRateObjectResult = new CurrencyExchangeRate
            {
                ISOCode = ISOCode.USD.GetDescriptionFromValue(),
                PurchaseExchangeRate = 375,
                SaleExchangeRate = 350
            };
        }

        [Given(@"the user (.*) has not bought currency (.*) in the last month")]
        public async Task GivenTheUserHasNotBoughtCurrencyInTheLastMonth(string userId, string isoCode)
        {
            _userId = Convert.ToUInt64(userId);

            var currencyPurchases = await _currencyPurchaseRepository.GetCurrencyPurchasesByUserInLastMonth(isoCode.GetEnumValueFromDescription<ISOCode>(), _userId);

            currencyPurchases.Should().BeEmpty();
        }

        [Given(@"I want to buy the currency (.*) for an amount of (.*) pesos")]
        public void GivenIWantToBuyForAnAmountO10000fPesos(string isoCode, string amountInPesos)
        {
            _amountToBuyInPesos = Convert.ToInt32(amountInPesos);
            _isoCode = isoCode;
        }

        [Given(@"the exchange rate is successfully retrieved")]
        public void GivenTheExchangeRateIsSuccessfullyRetrieved()
        {
            var mockedCurrencyExchangeRateResponse = new CurrencyExchangeRateApiResponse
            {
                PurchaseExchangeRate = 350,
                SaleExchangeRate = _saleExchangeRate,
                LastUpdate = "Test update"
            };
            _mockCurrencyApiService.Setup(x => x.RetrieveCurrencyExchangeRateAsync(It.IsAny<ISOCode>())).ReturnsAsync(mockedCurrencyExchangeRateResponse);
        }

        [Given(@"I bought 80 percent of the monthly limit for currency (.*) in the last month")]
        public async Task GivenIBought80PercentOfTheMonthlyLimitForCurrencyInTheLastMonth(string isoCode)
        {
            var enumIsoCode = isoCode.GetEnumValueFromDescription<ISOCode>();
            var currencyLimit = CurrenciesConstants.CurrenciesPurchaseLimits[enumIsoCode]; 
            var currencyPurchased = 80 * currencyLimit / 100;
            var amountInPesos = GetAmountInPesosRequiredToBuySpecificCurrency(currencyPurchased, enumIsoCode);

            await _currencyPurchaseRepository.CreateAsync(GetCurrencyPurchase(_defaultUserIdValue, amountInPesos, _saleExchangeRate, enumIsoCode, DateTime.Now.AddDays(-10)));
        }


        [Given(@"I try to buy 50 usd")]
        public void GivenITryToBuyUsd()
        {
            throw new PendingStepException();
        }


        [When(@"I try to perform the currency purchase")]
        public async Task WhenITryToPerformTheCurrencyPurchase()
        {

            try
            {
                _currencyPurchaseResult = await _currencyController.BuyCurrency(_isoCode, new API.Forms.PurchaseForm { PurchaseAmountInPesos = _amountToBuyInPesos, userId = UserIdHasNotBeenProvided() ? _defaultUserIdValue : _userId });
            }
            catch (Exception ex)
            {
                _exceptionResult = ex;
            }
        }

        [Then(@"I should see the purchase performed sucessfully")]
        public async Task ThenIShouldSeeThePurchasePerformedSucessfully()
        {
            var okResult = Assert.IsType<CreatedResult>(_currencyPurchaseResult);
            var resultValue = okResult.Value as CurrencyPurchase;

            var purchaseSummary = resultValue.AmountPurhcased;

            var enumIsoCode = _isoCode.GetEnumValueFromDescription<ISOCode>();
            var exchangeRate = enumIsoCode == ISOCode.USD ? _saleExchangeRate : _saleExchangeRate / 4;

            purchaseSummary.Should().NotBeNullOrEmpty();
            purchaseSummary.Should().ContainEquivalentOf($"{_amountToBuyInPesos / exchangeRate}");
        }

        private Infrastructure.Entities.CurrencyPurchase GetCurrencyPurchase(ulong userId, double purchaseAmountInPesos, double currencyExchangeRate, ISOCode isoCode, DateTime purchaseDate)
        {
            return new Infrastructure.Entities.CurrencyPurchase()
            {
                AmountInPesos = purchaseAmountInPesos,
                CurrencyExchangeRate = currencyExchangeRate,
                ISOCode = isoCode,
                UserId = userId,
                PurchaseDate = purchaseDate
            };
        }
        private double GetAmountInPesosRequiredToBuySpecificCurrency(double currencyPurchased, ISOCode isoCode)
        {
            return isoCode == ISOCode.USD ? currencyPurchased * _saleExchangeRate : currencyPurchased * (_saleExchangeRate / 4);
        }

        private bool UserIdHasNotBeenProvided() => _userId == 0;
    }
}
