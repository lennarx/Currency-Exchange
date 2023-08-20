using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMind.Exchange.Application.Services.Implementations.External.Responses
{
    public class CurrencyExchangeRateApiResponse
    {
        public double SaleExchangeRate { get; set; }
        public double PurchaseExchangeRate { get; set; }
        public string LastUpdate { get; set; }
    }
}
