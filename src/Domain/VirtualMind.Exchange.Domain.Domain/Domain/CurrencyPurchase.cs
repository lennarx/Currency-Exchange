namespace VirtualMind.Exchange.Domain
{
    public class CurrencyPurchase
    {
        public double PurchaseAmountInPesos { get; set; }
        public double CurrencyExchangeRate { get; set; }
        public string AmountPurhcased { get => $"{PurchaseAmountInPesos/CurrencyExchangeRate}"; }
    }
}
