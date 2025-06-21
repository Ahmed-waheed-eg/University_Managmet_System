using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISemesterRepository : IRepository<Semester>
    {
        Task<bool> CheckLevelExistsAsync(int levelId);
        Task<IEnumerable<Semester>> GetAllAsync();
       
        Task<IEnumerable<Semester>> GetAllByLevelIdAsync(int levelId);

    }
}
