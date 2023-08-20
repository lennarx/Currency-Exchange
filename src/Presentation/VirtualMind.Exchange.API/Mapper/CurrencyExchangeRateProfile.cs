using AutoMapper;
using VirtualMind.Exchange.Application.Services.Implementations.External.Responses;
using VirtualMind.Exchange.Domain.Domain.Domain;

namespace VirtualMind.Exchange.API.Mapper
{
    public class CurrencyExchangeRateProfile : Profile
    {
        public CurrencyExchangeRateProfile()
        {
            CreateMap<CurrencyExchangeRate, CurrencyExchangeRateApiResponse>().ReverseMap();
        }
    }
}
