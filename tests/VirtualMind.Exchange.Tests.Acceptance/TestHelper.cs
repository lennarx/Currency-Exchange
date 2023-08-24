using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Exchange.Application.Services.Settings.External;

namespace VirtualMind.Exchange.Tests.Acceptance
{
    public static class TestHelper
    {
        public static ExternalCurrencyExchangeRateServiceSettings GetMockedSettings()
        {
            return new ExternalCurrencyExchangeRateServiceSettings
            {
                ExternalServicesUrls = new List<ExternalServiceUrl>
                {
                    {new ExternalServiceUrl{ ISOCode = Infrastructure.Entities.Enums.ISOCode.USD, Url = "MockedUSDUrl"} },
                    {new ExternalServiceUrl{ ISOCode = Infrastructure.Entities.Enums.ISOCode.BRL, Url = "MockedBRLUrl"} }
                }
            };
        }   
    }
}
