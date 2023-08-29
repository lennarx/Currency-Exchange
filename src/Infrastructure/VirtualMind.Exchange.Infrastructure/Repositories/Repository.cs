using Microsoft.EntityFrameworkCore;
using VirtualMind.Exchange.Infrastructure.Entities;
using VirtualMind.Exchange.Infrastructure.Persistance.Context;

namespace VirtualMind.Exchange.Infrastructure.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly ExchangeDbContext _exchangeDbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(ExchangeDbContext exchangeDbContext)
        {
            _exchangeDbContext = exchangeDbContext;
            _dbSet = _exchangeDbContext.Set<TEntity>();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            if(entity == null) throw new ArgumentNullException(nameof(entity));

            _dbSet.Add(entity);
            await _exchangeDbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _exchangeDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(ulong id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            if(entity == null) throw new ArgumentNullException(nameof(entity));

            _dbSet.Update(entity);
            await _exchangeDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (_exchangeDbContext != null) _exchangeDbContext.Dispose();
        }
    }
}
