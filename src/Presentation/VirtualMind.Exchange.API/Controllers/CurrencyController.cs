using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualMind.Exchange.Application.Services.Implementations.External.Responses;
using VirtualMind.Exchange.Domain.Domain.Contracts.External;
using VirtualMind.Exchange.Domain.Domain.Domain;
using VirtualMind.Exchange.Domain.Domain.Domain.Enums;

namespace VirtualMind.Exchange.API.Controllers
{
    [Route("api/[controller]/{isoCode}")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyExchangeRateRetrieverService _currencyExchangeRateRetrieverService;
        private readonly IMapper _mapper;
        public CurrencyController(ICurrencyExchangeRateRetrieverService currencyExchangeRateRetrieverService, IMapper mapper)
        {

            _currencyExchangeRateRetrieverService = currencyExchangeRateRetrieverService;
            _mapper = mapper;

        }

        /// <summary>
        /// Gets the currency exchange rate for a given ISO code.
        /// </summary>
        /// <param name="isoCode">The currency ISO code that identifies the currency</param>
        /// <returns>The currency exchange rate</returns>
        /// <response code="200">Returns currrency exchange rate</response>
        /// <response code="400">If no valid ISO code was provided.</response>
        /// <response code="404">If no currency exchange rate was found</response>
        [HttpGet("exchangeRate")]
        [ProducesResponseType(typeof(CurrencyExchangeRate), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCurrencyExchangeRate(ISOCode isoCode)
        {
            var currencyExchangeRate = await _currencyExchangeRateRetrieverService.GetCurrencyExchangeRateAsync(isoCode);

            if (currencyExchangeRate is null)
            {
                return NotFound("No currency exchange rate was found for the given ISO code");
            }

            if (isoCode == ISOCode.BRL)
            {
                currencyExchangeRate.SaleExchangeRate = currencyExchangeRate.SaleExchangeRate / 4;
                currencyExchangeRate.PurchaseExchangeRate = currencyExchangeRate.PurchaseExchangeRate / 4;
            }            

            return Ok(_mapper.Map<CurrencyExchangeRate>(currencyExchangeRate));
        }


    }
}
