using VirtualMind.Exchange.Domain;
using VirtualMind.Exchange.Infrastructure.Entities.Enums;

namespace VirtualMind.Exchange.Application.Services.Contracts
{
    public interface ICurrencyService
    {
        Task<CurrencyExchangeRate> GetCurrencyExchangeRateAsync(ISOCode code);
        Task<CurrencyPurchase> BuyCurrencyAsync(ISOCode isoCode, ulong userId, double purchaseAmountInPesos);
    }
}
