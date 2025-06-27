using Application.Interfaces;
using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SemesterServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISemesterRepository _semesterRepository;
        public SemesterServices(IUnitOfWork unitOfWork, ISemesterRepository semesterRepository)
        {
            _unitOfWork = unitOfWork;
            _semesterRepository = semesterRepository;
        }

        public async Task<(bool Success, int id, string ErrorMessage)> CreateAsync(SemesterDTO dto)
        {
            var exists = await _semesterRepository.GetByNameAsync(dto.Name);
            if (exists != null)
            {
                return (false, 0, "This Semester already exists.");
            }
            if (dto.LevelId <= 0)
            {
                return (false, 0, "Invalid Level ID.");
            }
            // Check if the Level exists
            var levelExists = await _semesterRepository.CheckLevelExistsAsync(dto.LevelId);
            if (!levelExists)
            {
                return (false, 0, "Level ID does not exist.");
            }
            var semester = new Semester { Name = dto.Name, LevelId = dto.LevelId };
            await _semesterRepository.AddAsync(semester);
            if (await _unitOfWork.IsCompleteAsync())
            {
                return (true, semester.Id, "Created Successfully");
            }
            return (false, 0, "Error in saving changes");
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteAsync(int Id)
        {
            var semester = await _semesterRepository.GetByIdAsync(Id);
            if (semester == null)
            {
                return (false, "This ID not found.");
            }
            _semesterRepository.Delete(semester);
            if (await _unitOfWork.IsCompleteAsync())
            {
                return (true, "Deleted Successfully.");
            }
            return (false, "Error in saving changes");
        }

        public async Task<(bool Success, SemesterDTO dto, string message)> GetOneAsync(int Id)
        {
            var semester = await _semesterRepository.GetByIdAsync(Id);
            if (semester == null)
            {
                return (false, null, "This ID not found.");
            }
            var dto = new SemesterDTO
            {
                Id = semester.Id,
                Name = semester.Name,
                LevelId = semester.LevelId
            };
            return (true, dto, "Retrieved Successfully");
        }

        public async Task<IEnumerable<SemesterDTO>> GetAllAsync()
        {
            var semesters = await _semesterRepository.GetAllAsync();
            return semesters.Select(s => new SemesterDTO
            {
                Id = s.Id,
                Name = s.Name,
                LevelId = s.LevelId
            }).ToList();
        }


        public async Task<IEnumerable<SemesterDTO>> GetAllByLevelIdAsync(int levelId)
        {
            var semesters = await _semesterRepository.GetAllByLevelIdAsync(levelId);
            return semesters.Select(s => new SemesterDTO
            {
                Id = s.Id,
                Name = s.Name,
                LevelId = s.LevelId
            }).ToList();

        }


        public async Task<(bool Success, string message)> UpdateAsync(SemesterDTO dto)
        {
            var semester = await _semesterRepository.GetByIdAsync(dto.Id);
            if (semester == null)
            {
                return (false, "This ID not found.");
            }
            semester.Name = dto.Name;
            if (dto.LevelId <= 0)
            {
                return (false, "Invalid Level ID.");
            }
            semester.LevelId = dto.LevelId;
            // Check if the Level exists
            var levelExists = await _semesterRepository.CheckLevelExistsAsync(dto.LevelId);
            if (!levelExists)
            {
                return (false, "Level ID does not exist.");
            }
            _semesterRepository.Update(semester);
            if (await _unitOfWork.IsCompleteAsync())
            {
                return (true, "Updated Successfully");
            }
            return (false, "Error in saving changes");
        }

        public async Task<bool> ActiveSemesterAsync(int semesterId)
        {
            var semester = await _semesterRepository.GetByIdAsync(semesterId);
            if (semester == null||semester.IsActive)
            {
                return false;
            }
            return await _semesterRepository.ActiveSemesterAsync(semesterId);
        }

    }
}
