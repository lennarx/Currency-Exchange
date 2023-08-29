using VirtualMind.Exchange.Infrastructure.Entities;
using VirtualMind.Exchange.Infrastructure.Entities.Enums;
using VirtualMind.Exchange.Infrastructure.Persistance.Context;
using VirtualMind.Exchange.Infrastructure.Repositories.Currency.Contracts;

namespace VirtualMind.Exchange.Infrastructure.Repositories.Currency.Implementations
{
    public class CurrencyPurchaseRepository : Repository<CurrencyPurchase>, ICurrencyPurchaseRepository
    {
        public CurrencyPurchaseRepository(ExchangeDbContext exchangeDbContext) : base(exchangeDbContext)
        {
        }

        public async Task<IEnumerable<CurrencyPurchase>> GetCurrencyPurchasesByUserInLastMonth(ISOCode isoCode, ulong userId)
        {
            return _dbSet.Where(purchase => purchase.PurchaseDate >= DateTimeOffset.UtcNow.AddMonths(-1) && purchase.ISOCode == isoCode
                        && purchase.UserId == userId);            
        }
    }
}
