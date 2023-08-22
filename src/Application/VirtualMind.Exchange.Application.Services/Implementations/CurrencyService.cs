using Microsoft.Extensions.Logging;
using VirtualMind.Exchange.API.Exceptions;
using VirtualMind.Exchange.Application.Services.Contracts;
using VirtualMind.Exchange.Application.Services.Exceptions;
using VirtualMind.Exchange.Application.Services.Utils.Consts;
using VirtualMind.Exchange.Application.Services.Utils.Extensions;
using VirtualMind.Exchange.Domain;
using VirtualMind.Exchange.Domain.Domain.Contracts.External;
using VirtualMind.Exchange.Infrastructure.Entities.Enums;
using VirtualMind.Exchange.Infrastructure.Repositories.Currency.Contracts;

namespace VirtualMind.Exchange.Application.Services.Implementations
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyExchangeRateApiService _currencyExchangeRateRetrieverService;
        private readonly ILogger<CurrencyService> _logger;
        private readonly ICurrencyPurchaseRepository _currencyPurchaseRepository;
        public CurrencyService(ICurrencyExchangeRateApiService currencyExchangeRateApiService, ILogger<CurrencyService> logger,
            ICurrencyPurchaseRepository currencyPurchaseRepository)
        {
            _currencyExchangeRateRetrieverService = currencyExchangeRateApiService;
            _logger = logger;
            _currencyPurchaseRepository = currencyPurchaseRepository;
        }

        public async Task<CurrencyExchangeRate> GetCurrencyExchangeRateAsync(ISOCode isoCode)
        {
            var currencyExchangeRate = await _currencyExchangeRateRetrieverService.RetrieveCurrencyExchangeRateAsync(isoCode);

            if (currencyExchangeRate is null)
            {
                throw new NoCurrencyRateFoundException("No currency exchange rate was found for the given ISO code");
            }

            if (isoCode == ISOCode.BRL)
            {
                currencyExchangeRate.PurchaseExchangeRate = currencyExchangeRate.PurchaseExchangeRate / 4;
                currencyExchangeRate.SaleExchangeRate = currencyExchangeRate.SaleExchangeRate / 4;
            }

            return new CurrencyExchangeRate
            {
                ISOCode = isoCode.GetDescriptionFromValue(),
                PurchaseExchangeRate = currencyExchangeRate.PurchaseExchangeRate,
                SaleExchangeRate = currencyExchangeRate.SaleExchangeRate,
            };
        }

        public async Task<CurrencyPurchase> BuyCurrencyAsync(ISOCode isoCode, ulong userId, double purchaseAmountInPesos)
        {
            var amountPurchasedInLastMonth = await GetAmountPurchasedInLastMoth(isoCode, userId);

            ValidateIfAmountExceedsMonthLimit(amountPurchasedInLastMonth, isoCode);

            var currencyExchangeRate = await GetCurrencyExchangeRateAsync(isoCode);

            var amountToPurchase = purchaseAmountInPesos / currencyExchangeRate.SaleExchangeRate;

            ValidateIfAmountExceedsMonthLimit(amountPurchasedInLastMonth + amountToPurchase, isoCode); 

            var currencyPurchase = new Infrastructure.Entities.CurrencyPurchase
            {
                AmountInPesos = purchaseAmountInPesos,
                CurrencyExchangeRate = currencyExchangeRate.SaleExchangeRate,
                ISOCode = isoCode,
                PurchaseDate = DateTime.UtcNow,
                UserId = userId
            };

            _logger.LogInformation($"Buying currency: {isoCode}, amount in pesos: {purchaseAmountInPesos}," +
                $" purchase amount: {isoCode}{currencyPurchase.AmountPurchased}");

            await _currencyPurchaseRepository.CreateAsync(currencyPurchase);

            return new CurrencyPurchase { CurrencyExchangeRate = currencyExchangeRate.PurchaseExchangeRate, PurchaseAmountInPesos = purchaseAmountInPesos };
        }

        private async Task<double> GetAmountPurchasedInLastMoth(ISOCode isoCode, ulong userId)
        {
            var purchasesInLastMoth = await _currencyPurchaseRepository.GetCurrencyPurchasesByUserInLastMonth(isoCode, userId);

            return purchasesInLastMoth.Sum(x => x.AmountPurchased);
        }

        private void ValidateIfAmountExceedsMonthLimit(double amountToEvaluate, ISOCode isoCode)
        {
            if(amountToEvaluate > CurrenciesConstants.CurrenciesPurchaseLimits[isoCode])
            {
                throw new CurrencyPurchaseLimitExceededException(isoCode.GetDescriptionFromValue());
            }
        }
    }
}
