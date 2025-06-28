using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Application.DTOs;


namespace Infrastructure.Repositories
{
    public class GenericRepo<T> : IRepository<T> where T : class
    {
        private readonly  ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepo(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T Entity)
        {
            await _dbSet.AddAsync(Entity);
        }

        public void Delete(T Entity)
        {
             _dbSet.Remove(Entity);
        }

        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public void Update(T Entity)
        {
            _dbSet.Update(Entity);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task<bool> AnyAsync(Expression<Func<T,bool>>predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<PaginationDTO<T>> GetAllAsync(int pageNumber, int pageSize)
        {

            var totalCount = await _dbSet.CountAsync();
            var values = await _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return await Task.FromResult(new PaginationDTO<T>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                values = values
            });
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }


        public async Task<T> GetByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }


    

    }
}
