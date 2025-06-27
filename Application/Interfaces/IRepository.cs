using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepository<T>where T : class
    {
        Task<T>GetByIdAsync(int id);
        Task<T> GetByAsync(Expression<Func<T,bool>> predicate);
        Task AddAsync(T Entity);
        void Update(T Entity); 
        void Delete(T Entity);
        public void Commit();
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByNameAsync(string name);
        Task<IEnumerable<T>> GetAllAsync();
        
        Task<PaginationDTO<T>> GetAllAsync(int pageNumber, int pageSize);

        //new with Expression
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByExpesAsync(Expression<Func<T, bool>> predicate);
    }
}
