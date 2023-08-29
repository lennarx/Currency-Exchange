using Microsoft.AspNetCore.Mvc;
using VirtualMind.Exchange.API.Forms;
using VirtualMind.Exchange.Application.Services.Contracts;
using VirtualMind.Exchange.Application.Services.Utils.Extensions;
using VirtualMind.Exchange.Domain;
using VirtualMind.Exchange.Infrastructure.Entities.Enums;

namespace VirtualMind.Exchange.API.Controllers
{
    [Route("api/[controller]/{isoCode}")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
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
        public async Task<IActionResult> GetCurrencyExchangeRate([FromRoute] string isoCode)
        {
            var currencyExchangeRate = await _currencyService.GetCurrencyExchangeRateAsync(isoCode.ConvertStringISOCodeToEnum());

            return Ok(currencyExchangeRate);
        }

        /// <summary>
        /// Purchase the currency selected in the provided iso code at the current exchange rate in pesos
        /// </summary>
        /// <param name="isoCode">The currency ISO code that identifies the currency</param>
        /// <param name="purchaseForm">The form for the purchase which contains the user id and the amount in pesos to buy</param>
        /// <response code="201">The purchase was performed successfully</response>
        /// <response code="400">If no valid ISO code was provided.</response>
        /// <response code="400">If the purchase exceeds the 200 monthly limit.</response>
        /// <response code="404">If no currency exchange rate was found</response>
        [HttpPost("purchase")]
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BuyCurrency([FromRoute] string isoCode, [FromBody] PurchaseForm purchaseForm)
        {
           var currencyPurchase = await _currencyService.BuyCurrencyAsync(isoCode.ConvertStringISOCodeToEnum(), purchaseForm.userId, purchaseForm.PurchaseAmountInPesos);

            return Created(nameof(BuyCurrency), currencyPurchase);
        }
    }
}
