
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;


namespace Infrastructure.Repositories
{
    public class EnrollmentRepository(ApplicationDbContext dbContext) : GenericRepo<Enrollment>(dbContext), IEnrollmentRepository
    {
    }
}
