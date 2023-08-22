using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMind.Exchange.Application.Services.Exceptions
{
    public class CurrencyPurchaseLimitExceededException : Exception
    {
        public CurrencyPurchaseLimitExceededException(string isoCode) : base($"The amount of {isoCode} currency you're trying to buy exceeds your month limit"){ }
    }
}
