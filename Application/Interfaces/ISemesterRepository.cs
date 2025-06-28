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
      
       
        Task<bool> ActiveSemesterAsync(int SemesterID);

    }
}
