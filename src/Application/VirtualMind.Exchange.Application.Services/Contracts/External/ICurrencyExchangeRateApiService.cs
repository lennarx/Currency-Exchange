using VirtualMind.Exchange.Application.Services.Implementations.External.Responses;
using VirtualMind.Exchange.Infrastructure.Entities.Enums;

namespace VirtualMind.Exchange.Domain.Domain.Contracts.External
{
    public interface ICurrencyExchangeRateApiService
    {     
        Task<CurrencyExchangeRateApiResponse> RetrieveCurrencyExchangeRateAsync(ISOCode isoCode);
    }
}
