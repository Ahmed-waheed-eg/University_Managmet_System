using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AdminRepository(ApplicationDbContext _Context) : GenericRepo<Admin>(_Context), IAdminRepository
    {
        public Task<Admin> GetByEmailAsync(string email)
        {

            return _Context.Admins
                .FirstOrDefaultAsync(admin => admin.Email == email);
        }

    }
}
