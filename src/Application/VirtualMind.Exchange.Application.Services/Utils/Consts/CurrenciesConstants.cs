using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Exchange.Infrastructure.Entities.Enums;

namespace VirtualMind.Exchange.Application.Services.Utils.Consts
{
    public static class CurrenciesConstants
    {
        public static IDictionary<ISOCode, double> CurrenciesPurchaseLimits = new Dictionary<ISOCode, double> { { ISOCode.USD, 200}, { ISOCode.BRL, 300} };
    }
}
