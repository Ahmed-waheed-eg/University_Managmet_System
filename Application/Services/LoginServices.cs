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
        (ISuperAdminRepository _superAdminRepository,
        IPasswordHasher _passwordHasher,
        IStudentRepository _studentRepository,
        IProfessorRepository _professorRepository,
        IAdminRepository _adminRepository,
        TokenService _tokenService)
    {
       
        public async Task<AuthresponsetDTO> LoginSuperAdminAsync(string Email, string password)
        {

            var superAdmin = await _superAdminRepository.GetByAsync(s=>s.Email==Email);
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


        public async Task<AuthresponsetDTO> LoginAdminAsync(string Email, string password)
        {
            var admin = await _adminRepository.GetByAsync(s => s.Email == Email);
            if (admin == null)
            {
                return null; // User not found
            }
            if (!_passwordHasher.VerifyPassword(password, admin.PasswordHash))
            {
                return null; // Invalid password
            }
            var token = _tokenService.CreateToken(admin.Id, admin.Name, Domain.Enums.UserRole.Admin);
            return new AuthresponsetDTO
            {
                Token = token,
                FullName = admin.Name,
                Role = "Admin"
            };
        }


        public async Task<AuthresponsetDTO> LoginStudentAsync(string Email, string password)
        {
            var student = await _studentRepository.GetByAsync(s => s.Email == Email);
            if (student == null)
            {
                return null; // User not found
            }
            if (!_passwordHasher.VerifyPassword(password, student.PasswordHash))
            {
                return null; // Invalid password
            }
            var token = _tokenService.CreateToken(student.Id, student.Name, Domain.Enums.UserRole.Student);
            return new AuthresponsetDTO
            {
                Token = token,
                FullName = student.Name,
                Role = "Student"
            };
        }


        public async Task<AuthresponsetDTO> LoginProfessorAsync(string Email, string password)
        {
            var professor = await _professorRepository.GetByAsync(s => s.Email == Email);
            if (professor == null)
            {
                return null; // User not found
            }
            if (!_passwordHasher.VerifyPassword(password, professor.HashPassword))
            {
                return null; // Invalid password
            }
            var token = _tokenService.CreateToken(professor.Id, professor.Name, Domain.Enums.UserRole.Doctor);
            return new AuthresponsetDTO
            {
                Token = token,
                FullName = professor.Name,
                Role = "Doctor"
            };
        }


    }
}
