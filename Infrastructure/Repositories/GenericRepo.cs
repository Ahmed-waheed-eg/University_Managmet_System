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

        public async Task<T> GetByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(e => EF.Property<string>(e, "Name") == name);

        }
    }
}
