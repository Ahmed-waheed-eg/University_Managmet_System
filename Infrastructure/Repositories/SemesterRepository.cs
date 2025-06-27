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

        public async Task<bool> ActiveSemesterAsync(int semesterID)
        {
            var semester = await _context.Semesters.FindAsync(semesterID);
            if (semester == null)
            {
                return false;
            }
            var currentActiveSemester = await _context.Semesters.Where(s => s.IsActive).ToListAsync();
            if (currentActiveSemester.Count > 0)
            {
                foreach (var item in currentActiveSemester)
                {
                    item.IsActive = false;
                    _context.Semesters.Update(item);
                }
            }
            var ActivatedSemesters = await _context.Semesters.Where(s => s.Name == semester.Name).ToListAsync();

            if (ActivatedSemesters.Count > 0)
            {
                foreach (var item in ActivatedSemesters)
                {
                    item.IsActive = true;
                    _context.Semesters.Update(item);
                }
            }


            return await _context.SaveChangesAsync() > 0;
        }

    }
}
