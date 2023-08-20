using VirtualMind.Exchange.Application.Services.Implementations.External;
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
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

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
