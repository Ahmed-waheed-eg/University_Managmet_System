using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CreateUseresServices
        (IStudentRepository _studentRepository,
        IDepartmentRepositiry _departmentRepositiry,
            IProfessorRepository _professorRepository,
            ICourseRepository _courseRepository,
         IUnitOfWork _unitOfWork,
         IPasswordHasher _PasswordHasher
        )
    {


        public async Task<(bool Success, int id, string ErrorMessage)> CreateStudent(CreateStudentDTO student)
        {
            var exists = await _studentRepository.GetByAsync(a => a.Name == student.Name);
            if (exists != null)
            {
                return (false, 0, "This User already Exist.");
            }
            var existsEmail = await _studentRepository.GetByAsync(a => a.Email == student.Email);
            if (existsEmail != null)
            {
                return (false, 0, "This Email already Exist.");
            }
            var Department = _departmentRepositiry.AnyAsync(d => d.Id == student.DepartmentId);
            if (!Department.Result)
            {
                return (false, 0, "This Department does not Exist.");
            }

            var Student = new Student
            {
                Name = student.Name,
                Email = student.Email,
                NationalId = student.NationalId,
                PasswordHash = _PasswordHasher.HashPassword(student.Password),
                DepartmentId = student.DepartmentId
            };
            await _studentRepository.AddAsync(Student);
            if (await _unitOfWork.IsCompleteAsync())
            {
                return (true, Student.Id, "Created Successfully");
            }

            return (false, 0, "Error in saving setting");

        }

        public async Task<(bool Success, int id, string ErrorMessage)> CreateProfessor(CreateProfessorDTO professor)
        {
            var exists = await _professorRepository.GetByAsync(a => a.Name == professor.Name);
            if (exists != null)
            {
                return (false, 0, "This User already Exist.");
            }
            var existsEmail = await _professorRepository.GetByAsync(a => a.Email == professor.Email);
            if (existsEmail != null)
            {
                return (false, 0, "This Email already Exist.");
            }
            var Department = _departmentRepositiry.AnyAsync(d => d.Id == professor.DepartmentId);
            if (!Department.Result)
            {
                return (false, 0, "This Department does not Exist.");
            }
            var Course = _courseRepository.AnyAsync(c => c.Id == professor.CourseId);
            if (!Course.Result)
            {
                return (false, 0, "This Course does not Exist.");
            }
            var Professor = new Professor
            {
                Name = professor.Name,
                Email = professor.Email,
                HashPassword = _PasswordHasher.HashPassword(professor.Password),
                PhoneNumber = professor.PhoneNumber,
                DepartmentId = professor.DepartmentId,
                CourseId = professor.CourseId
            };
            await _professorRepository.AddAsync(Professor);
            var m = await _unitOfWork.IsCompleteAsyncWithError();
            if(m.Success)
            return (true, Professor.Id, "Created Successfully");

            return (false, 0, m.ErrorMessage);
        }
    }
}
