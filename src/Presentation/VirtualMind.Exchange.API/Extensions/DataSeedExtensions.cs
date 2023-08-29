using Microsoft.EntityFrameworkCore;
using VirtualMind.Exchange.Infrastructure.Persistance.Context;

namespace VirtualMind.Exchange.API.Extensions
{
    public static class DataSeedExtensions
    {
        private static IServiceProvider _provider;

        public static async Task SeedData(this IApplicationBuilder builder)
        { //This line of code

            _provider = builder.ApplicationServices;

            using (var context = _provider.GetService<ExchangeDbContext>())
            {

                await context.Database.MigrateAsync();
            }
        }
    }
}
