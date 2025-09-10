using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ResultPattern;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IGenericRepo<T> where T : IEntityId
    {
        Task<Result<List<T>>> GetAllAsync();
        Task<Result<T>> GetById(int id);
        Task<Result> AddAsync(T item);
        Result<T> UpdateAsync(T item);
        void Remove(T item);
    }
}
