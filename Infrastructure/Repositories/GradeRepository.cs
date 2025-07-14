using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GradeRepository(ApplicationDbContext _Context) : GenericRepo<Grade>(_Context),IGradeRepository
    {
    }
}
