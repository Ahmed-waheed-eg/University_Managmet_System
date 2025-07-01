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

        public async Task<bool> ActiveSemesterAsync(int semesterOrder)
        {
           await _context.Database.ExecuteSqlRawAsync("UPDATE Semesters SET IsActive=0");

            var activeSemester = await _context.Semesters.Where(s => s.Order == semesterOrder).ToListAsync();
            if (activeSemester.Any())
            {
                foreach (var semester in activeSemester)
                {
                    semester.IsActive = true;
                    _context.Semesters.Update(semester);
                }
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeActiveSemesterAsync(int semesterOrder)
        {
            var Semesters = _context.Semesters.Where(s => s.Order == semesterOrder).ToList();
            if (Semesters.Any())
            {
                foreach (var semester in Semesters)
                {
                    semester.IsActive = false;
                    _context.Semesters.Update(semester);
                }
            }
            return await _context.SaveChangesAsync() > 0;


        }
    }
}
