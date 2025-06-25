using Domain.Entities;
using Application.Interfaces;
using Application.DTOs;

namespace Application.Services
{

    public class CreatedUseresServices
    {
        private readonly ISuperAdminRepository _superAdminRepository;
        private readonly IPasswordHasher _PasswordHasher;
        private readonly IUnitOfWork _unitOfWork;
        public CreatedUseresServices(ISuperAdminRepository superAdminRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _superAdminRepository = superAdminRepository;
            _unitOfWork = unitOfWork;
            _PasswordHasher = passwordHasher;
        }

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




    }
}
