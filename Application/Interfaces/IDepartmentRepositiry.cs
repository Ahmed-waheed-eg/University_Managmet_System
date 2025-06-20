using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDepartmentRepositiry : IRepository<Department>
    {
        Task<Department> GetByName(string name);
        Task<IEnumerable<Department>> GetAllAsync();
    }
}
