using Domain.Entities;
using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CourseRepository(ApplicationDbContext _context) : GenericRepo<Course>(_context), ICourseRepository
    {
    }
}
