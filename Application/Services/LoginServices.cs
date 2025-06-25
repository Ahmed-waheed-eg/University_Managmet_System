using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;


namespace Application.Services
{
    public class LoginServices 
    {
        private readonly ISuperAdminRepository _superAdminRepository;
        private readonly IPasswordHasher _passwordHasher;
        public readonly TokenService _tokenService;
        public LoginServices(ISuperAdminRepository superAdminRepository, IPasswordHasher passwordHasher, TokenService tokenService)
        {
            _superAdminRepository = superAdminRepository;
            _passwordHasher = passwordHasher;
            this._tokenService = tokenService;
        }
        public async Task<AuthresponsetDTO> LoginSuperAdminAsync(string Email, string password)
        {

            var superAdmin = await _superAdminRepository.GetByEmailAsync(Email);
            if (superAdmin == null)
            {
                return null; // User not found
            }
            if (!_passwordHasher.VerifyPassword(password, superAdmin.PasswordHash))
            {
                return null; // Invalid password
            }
            var token = _tokenService.CreateToken(superAdmin.Id,superAdmin.Name,Domain.Enums.UserRole.SuperAdmin);
            return new AuthresponsetDTO
            {
                Token = token,
                FullName = superAdmin.Name,
                Role = "SuperAdmin"
            };

        }
    }
}
