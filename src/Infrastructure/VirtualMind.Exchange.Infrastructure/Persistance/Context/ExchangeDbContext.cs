using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Exchange.Infrastructure.Entities;
using VirtualMind.Exchange.Infrastructure.Persistance.Context.Configuration;

namespace VirtualMind.Exchange.Infrastructure.Persistance.Context
{
    public class ExchangeDbContext : DbContext
    {
        public ExchangeDbContext(DbContextOptions<ExchangeDbContext> options) : base(options) { }

        public DbSet<CurrencyPurchase> CurrencyPurchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CurrencyPurchaseConfiguration());
        }
    }
}
