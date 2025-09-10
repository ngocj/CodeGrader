using Common.ResultPattern;
using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> 
        where T : class, IEntityId
    {
        protected readonly ProgressContext _context;

        public GenericRepo(ProgressContext context)
        {
            _context = context;
        }

        public async Task<Result> AddAsync(T item)
        {
            try
            {
                await _context.Set<T>().AddAsync(item);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public void Remove(T item)
        {
            _context.Set<T>().Remove(item);
        }

        public async Task<Result<List<T>>> GetAllAsync()
        {
            try
            {
                var list = await _context.Set<T>().ToListAsync();
                return Result<List<T>>.Success(list);
            }
            catch (Exception ex)
            {
                return Result<List<T>>.Failure(ex.Message);
            }
        }

        public async Task<Result<T>> GetById(int id)
        {
            try
            {
                var item = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
                return Result<T>.Success(item);
            }
            catch (Exception ex)
            {
                return Result<T>.Failure(ex.Message);
            }
        }

        public Result<T> UpdateAsync(T item)
        {
            try
            {
                _context.Update<T>(item);
                return Result<T>.Success(item);
            }
            catch (Exception ex)
            {
                return Result<T>.Failure(ex.Message);
            }
        }
    }
}
