using Domain.Entities;
using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CourseRepository : GenericRepo<Course>, ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

        public Task<Course> GetByCodeAsync(string Code)
        {

            return _context.Courses.FirstOrDefaultAsync(c => c.Code == Code);
        }
    }
}
