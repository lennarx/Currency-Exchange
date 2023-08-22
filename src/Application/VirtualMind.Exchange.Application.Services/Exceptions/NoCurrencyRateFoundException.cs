namespace VirtualMind.Exchange.API.Exceptions
{
    public class NoCurrencyRateFoundException : Exception
    {
        public NoCurrencyRateFoundException(string isoCode) : base($"No exchange rate was found for the given currency {isoCode}") { }
    }
}
