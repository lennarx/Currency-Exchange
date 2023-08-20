using VirtualMind.Exchange.Application.Services.Implementations.External.Responses;
using VirtualMind.Exchange.Domain.Domain.Domain;
using VirtualMind.Exchange.Domain.Domain.Domain.Enums;

namespace VirtualMind.Exchange.Domain.Domain.Contracts.External
{
    public interface ICurrencyExchangeRateRetrieverService
    {     
        Task<CurrencyExchangeRateApiResponse> GetCurrencyExchangeRateAsync(ISOCode isoCode);
    }
}
