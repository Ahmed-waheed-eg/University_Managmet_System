using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SuperAdminRepository
     : GenericRepo<SuperAdmin>, ISuperAdminRepository
    {
      private readonly ApplicationDbContext _context;
        public SuperAdminRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<SuperAdmin> GetByEmailAsync(string email)
        {

            return _context.SuperAdmins
                .FirstOrDefaultAsync(admin => admin.Email == email);
        }
    }
}
