using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SemesterRepository(ApplicationDbContext context) : GenericRepo<Semester>(context), ISemesterRepository
    {
        private readonly ApplicationDbContext _context = context;

        public Task<bool> CheckLevelExistsAsync(int levelId)
        {

            return _context.Levels.AnyAsync(l => l.Id == levelId);
        }

        public async Task<IEnumerable<Semester>> GetAllAsync()
        {
            return await _context.Semesters.OrderBy(x => x.Name).ToListAsync();

        }

        public async Task<IEnumerable<Semester>> GetAllByLevelIdAsync(int levelId)
        {

            return await _context.Semesters
                .Where(s => s.LevelId == levelId)
                .OrderBy(s => s.Name)
                .ToListAsync();

        }
    }
}
