using Application.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UniteOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        
        public UniteOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> CompleteAsync()=>_context.SaveChangesAsync();
        public async Task<bool> IsCompleteAsync()
        {
            try
            {
                int affectedRowa = await _context.SaveChangesAsync();
                return affectedRowa > 0;
            }
            catch (Exception)
            { return false; }
        
        }

        // with eror mesage 
        public async Task<(bool Success, string ErrorMessage)> IsCompleteAsyncWithError()
        {
            try
            {
                int affectedRowa = await _context.SaveChangesAsync();
                return (affectedRowa > 0, "Operation completed successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occurred: {ex.Message}");
            }
        }
    }
}
