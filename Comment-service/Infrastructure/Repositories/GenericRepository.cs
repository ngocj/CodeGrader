using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CMContext _cMContext;

        public GenericRepository(CMContext cMContext)
        {
            _cMContext = cMContext;
        }

        public async Task AddAsync(T entity)
        {
            await _cMContext.Set<T>().AddAsync(entity);    
        }

        public Task DeleteAsync(T entity)
        {
             _cMContext.Set<T>().Remove(entity);
             return Task.CompletedTask;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _cMContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await _cMContext.Set<T>().FindAsync(id);
        }

        public Task UpdateAsync(T entity)
        {
            _cMContext.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
