using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StudentServices(IStudentRepository _studentRepository,
        IDepartmentRepositiry _departmentRepositiry,
        IUnitOfWork _unitOfWork,
        IPasswordHasher _PasswordHasher
    )
    {

        public async Task<StudentDTO> GetStudentAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return null;
            }
            return new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                NationalId = student.NationalId,
                DepartmentId = student.DepartmentId
            };
        }

        public async Task<IEnumerable<StudentDTO>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return students.Select(s => new StudentDTO
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                NationalId = s.NationalId,
                DepartmentId = s.DepartmentId
            });
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteStudentAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return (false, "Student not found.");
            }
            _studentRepository.Delete(student);
            if (await _unitOfWork.IsCompleteAsync())
            {
                return (true, "Student deleted successfully.");
            }
            return (false, "Error deleting student.");
        }

    }
}
