using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Exchange.Domain.Domain.Domain.Enums;

namespace VirtualMind.Exchange.Domain.Domain.Domain
{
    public class CurrencyExchangeRate
    {
        public double SaleExchangeRate { get; set; }
        public double PurchaseExchangeRate { get; set; }
    }
}
