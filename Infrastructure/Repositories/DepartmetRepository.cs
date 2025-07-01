using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DepartmetRepository : GenericRepo<Department>,IDepartmentRepositiry
    {
        private readonly ApplicationDbContext _context;
        public DepartmetRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<Department> GetDepartmentWithLevelsAndSemestersAsync(int departmentId)
        {

            return _context.Departments
                .Include(d => d.Levels)
                    .ThenInclude(l => l.Semesters)
                .FirstOrDefaultAsync(d => d.Id == departmentId);
        }
    }
}
