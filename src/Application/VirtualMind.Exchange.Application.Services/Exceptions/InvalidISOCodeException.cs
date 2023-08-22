namespace VirtualMind.Exchange.API.Exceptions
{
    public class InvalidISOCodeException : Exception
    {
        public InvalidISOCodeException(string isoCode) : base($"No currency was found for the provided code: {isoCode}") { }
    }
}
