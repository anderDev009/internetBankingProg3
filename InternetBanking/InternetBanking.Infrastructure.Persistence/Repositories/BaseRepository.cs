using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        private readonly InternetBankingContext _ctx;
        public BaseRepository(InternetBankingContext ctx)
        {
            _ctx = ctx;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _ctx.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAllWithInclude(List<string> properties)
        {
            var query = _ctx.Set<T>().AsQueryable();
            foreach (string property in properties)
            {
                query.Include(property);
            }
            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _ctx.Set<T>().FindAsync(id);
        }

        public async Task RemoveAsync(T entity)
        {
            _ctx.Set<T>().Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public virtual async Task<T> SaveAsync(T entity)
        {
            _ctx.Set<T>().Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity, int id)
        {
            T entry = await _ctx.Set<T>().FindAsync(id);
            _ctx.Entry(entry).CurrentValues.SetValues(entity);
            await _ctx.SaveChangesAsync();
            return entry;
        }
    }
}
