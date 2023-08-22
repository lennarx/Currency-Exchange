using VirtualMind.Exchange.Infrastructure.Entities.Enums;

namespace VirtualMind.Exchange.Domain
{
    public class CurrencyExchangeRate
    {
        public string ISOCode { get; set; }
        public double SaleExchangeRate { get; set; }
        public double PurchaseExchangeRate { get; set; }
    }
}
