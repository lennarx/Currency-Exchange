using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Exchange.Infrastructure.Entities;

namespace VirtualMind.Exchange.Infrastructure.Persistance.Context.Configuration
{
    public class CurrencyPurchaseConfiguration : IEntityTypeConfiguration<CurrencyPurchase>
    {
        public void Configure(EntityTypeBuilder<CurrencyPurchase> builder)
        {
            builder.ToTable("CURRENCY_PURCHASE");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("ID");
            builder.Property(p => p.AmountInPesos).HasColumnName("AMOUNT_IN_PESOS");
            builder.Property(p => p.CurrencyExchangeRate).HasColumnName("CURRENCY_EXCHANGE_RATE");
            builder.Property(p => p.ISOCode).HasColumnName("ISO_CODE");
            builder.Property(p => p.UserId).HasColumnName("USER_ID");
            builder.Property(p => p.PurchaseDate).HasColumnName("PURCHASE_DATE");
        }
    }
}
