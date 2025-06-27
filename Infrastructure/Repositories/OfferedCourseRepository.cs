using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class OfferedCourseRepository(ApplicationDbContext _Context) : GenericRepo<OfferedCourse>(_Context),IOfferedCourseRepository
    {

    }
}
