using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Exchange.Infrastructure.Entities;
using VirtualMind.Exchange.Infrastructure.Entities.Enums;

namespace VirtualMind.Exchange.Infrastructure.Repositories.Currency.Contracts
{
    public interface ICurrencyPurchaseRepository : IRepository<CurrencyPurchase>
    {
        Task<IEnumerable<CurrencyPurchase>> GetCurrencyPurchasesByUserInLastMonth(ISOCode isoCode, ulong userId);
    }
}
