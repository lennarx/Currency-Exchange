using Microsoft.EntityFrameworkCore;
using VirtualMind.Exchange.API.Middlewares;
using VirtualMind.Exchange.Application.Services.Contracts;
using VirtualMind.Exchange.Application.Services.Implementations;
using VirtualMind.Exchange.Application.Services.Implementations.External;
using VirtualMind.Exchange.Application.Services.Settings.External;
using VirtualMind.Exchange.Domain.Domain.Contracts.External;
using VirtualMind.Exchange.Infrastructure.Persistance.Context;
using VirtualMind.Exchange.Infrastructure.Repositories.Currency.Contracts;
using VirtualMind.Exchange.Infrastructure.Repositories.Currency.Implementations;

namespace VirtualMind.Exchange.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
            var connectionString = $"Data source={dbHost}; Initial Catalog={dbName}; TrustServerCertificate=True; User ID =sa; Password={dbPassword}";

            services.AddDbContext<ExchangeDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.Configure<ExternalCurrencyExchangeRateServiceSettings>(_configuration.GetSection("Currencies"));

            services.AddScoped<ICurrencyExchangeRateApiService, CurrencyExchangeRateApiService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<ICurrencyPurchaseRepository, CurrencyPurchaseRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExchangeExceptionMiddleware(LoggerFactory.Create(builder => builder.AddConsole()));
            app.UseHttpsRedirection();

            app.UseAuthorization();
        }
    }
}
