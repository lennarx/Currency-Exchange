namespace VirtualMind.Exchange.Domain
{
    public class CurrencyPurchase
    {
        public double PurchaseAmountInPesos { get; set; }
        public double CurrencyExchangeRate { get; set; }
        public string PurchaseSummary { get => $"{PurchaseAmountInPesos}/{CurrencyExchangeRate}"; }
    }
}
