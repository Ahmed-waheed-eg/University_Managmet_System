using System;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISuperAdminRepository:IRepository<SuperAdmin>, IUseresInterface<SuperAdmin>
    {
        
    }
}
