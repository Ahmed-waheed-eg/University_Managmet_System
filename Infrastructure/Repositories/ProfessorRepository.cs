using Domain.Entities;
using Application.Interfaces;
using Infrastructure.Data;




namespace Infrastructure.Repositories
{
    public class ProfessorRepository(ApplicationDbContext dbContext) : GenericRepo<Professor>(dbContext),IProfessorRepository
    {
    }
}
