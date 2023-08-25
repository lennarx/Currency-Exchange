using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Exchange.Application.Services.Settings.External;
using VirtualMind.Exchange.Infrastructure.Persistance.Context;

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

        public static async Task<ExchangeDbContext> GetInMemoryCurrencyRepositoryTestAsync()
        {
            var contextOptions = new DbContextOptionsBuilder<ExchangeDbContext>()
                .UseInMemoryDatabase("ExchangeInMemoryDbTest")
                .Options;

            var context = new ExchangeDbContext(contextOptions);

            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();            

            return context;
        }
    }
}
