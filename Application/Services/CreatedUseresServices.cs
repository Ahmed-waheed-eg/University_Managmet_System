using Domain.Entities;
using Application.Interfaces;
using Application.DTOs;

namespace Application.Services
{

    public class CreatedUseresServices(
        ISuperAdminRepository _superAdminRepository,
        IAdminRepository _adminRepository,
        IUnitOfWork _unitOfWork,
        IPasswordHasher _PasswordHasher) 
    {
        

        public async Task<(bool Success, int id, string ErrorMessage)> CreateSuperAdmin(CreatesdUsersDTO superAdmin)
        {
            var exists = await _superAdminRepository.GetByNameAsync(superAdmin.Name);
            if (exists != null)
            {
                return (false, 0, "This User already Exist.");
            }

            var SuperAdmin=new SuperAdmin
            {
                Name = superAdmin.Name,
                Email = superAdmin.Email,
                PhoneNumber="447738",
                PasswordHash = _PasswordHasher.HashPassword(superAdmin.Password),
            };


            await _superAdminRepository.AddAsync(SuperAdmin);
            if (await _unitOfWork.IsCompleteAsync())
            {
                return (true, SuperAdmin.Id, "Created Successfully");
            }
            return (false, 0, "Error in saving setting");
        }


        public async Task<(bool Success, int id, string ErrorMessage)> CreateAdmin(CreatesdUsersDTO admin)
        {
            var exists = await _adminRepository.GetByNameAsync(admin.Name);
            if (exists != null)
            {
                return (false, 0, "This User already Exist.");
            }
            var Admin = new Admin
            {
                Name = admin.Name,
                Email = admin.Email,
                PhoneNumber = "447738",
                PasswordHash = _PasswordHasher.HashPassword(admin.Password),
            };
            await _adminRepository.AddAsync(Admin);
            if (await _unitOfWork.IsCompleteAsync())
            {
                return (true, Admin.Id, "Created Successfully");
            }
            return (false, 0, "Error in saving setting");
        }


    }
}
