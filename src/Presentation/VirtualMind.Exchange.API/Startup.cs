using VirtualMind.Exchange.Application.Services.Implementations.External;
using VirtualMind.Exchange.Application.Services.Settings.External;
using VirtualMind.Exchange.Domain.Domain.Contracts.External;

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

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.Configure<ExternalCurrencyExchangeRateServiceSettings>(_configuration.GetSection("Currencies"));

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<ICurrencyExchangeRateRetrieverService, CurrencyExchangeRateRetrieverService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseAuthorization();
        }
    }
}
