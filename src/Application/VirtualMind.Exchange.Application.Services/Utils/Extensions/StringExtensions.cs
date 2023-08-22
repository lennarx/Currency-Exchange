using VirtualMind.Exchange.API.Exceptions;
using VirtualMind.Exchange.Infrastructure.Entities.Enums;

namespace VirtualMind.Exchange.Application.Services.Utils.Extensions
{
    public static class StringExtensions
    {
        public static ISOCode ConvertStringISOCodeToEnum(this string isoCode)
        {
            ISOCode isoCodeEnumValue;

            try
            {
                isoCodeEnumValue = isoCode.GetEnumValueFromDescription<ISOCode>();
            }
            catch (Exception)
            {
                throw new InvalidISOCodeException(isoCode);
            }

            return isoCodeEnumValue;
        }
    }
}
