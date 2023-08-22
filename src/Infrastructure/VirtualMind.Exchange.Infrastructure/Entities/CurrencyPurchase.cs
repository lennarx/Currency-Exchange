using System.ComponentModel.DataAnnotations.Schema;
using VirtualMind.Exchange.Infrastructure.Entities.Enums;

namespace VirtualMind.Exchange.Infrastructure.Entities
{
    public class CurrencyPurchase : Entity
    {
       public double AmountInPesos { get; set; }
       public double CurrencyExchangeRate { get; set; }
       public ISOCode ISOCode { get; set; }
       public ulong UserId { get; set; }
       public DateTimeOffset PurchaseDate { get; set; }
       [NotMapped]
       public double AmountPurchased { get => AmountInPesos / CurrencyExchangeRate; }

    }
}
