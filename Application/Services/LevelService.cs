using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LevelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILevelRepositiry _levelRepositiry;
        private readonly ISemesterRepository _semesterRepository;
        public LevelService(IUnitOfWork unitOfWork, ILevelRepositiry levelRepositiry, ISemesterRepository semesterRepository)
        {
            _unitOfWork = unitOfWork;
            _levelRepositiry = levelRepositiry;
            _semesterRepository = semesterRepository;
        }

        public async Task<(bool Success, int id, string ErrorMessage)> CreateAsync(LevelDTO dto)
        {
            var exists = await _levelRepositiry.GetByNameAsync(dto.Name);
            if (exists != null)
            {
                return (false, 0, "This Level already exists.");
            }
            var level = new Level { Name = dto.Name, DepartmentId = dto.DepartmentId };
            await _levelRepositiry.AddAsync(level);
            if (await _unitOfWork.IsCompleteAsync())
            {
                return (true, level.Id, "Created Successfully");
            }
            return (false, 0, "Error in saving changes");
        }


        public async Task<(bool Success, string ErrorMessage)> DeleteAsync(int Id)
        {
            var level = await _levelRepositiry.GetByIdAsync(Id);
            if (level == null)
            {
                return (false, "This ID not found.");
            }
            _levelRepositiry.Delete(level);
            if (await _unitOfWork.IsCompleteAsync())
            {
                return (true, "Deleted Successfully.");
            }
            return (false, "Error in saving changes");
        }

        public async Task<(bool Success, string message)> UpdateAsync(LevelDTO dto)
        {
            var level = await _levelRepositiry.GetByIdAsync(dto.Id);
            if (level == null)
            {
                return (false, "This ID not found.");
            }
            var exists = await _levelRepositiry.GetByNameAsync(dto.Name);
            if (exists != null && exists.Id != dto.Id)
            {
                return (false, "This Level already exists.");
            }
            level.Name = dto.Name;
            level.DepartmentId = dto.DepartmentId;
            _levelRepositiry.Update(level);
            if (await _unitOfWork.IsCompleteAsync())
            {
                return (true, "Updated Successfully");
            }
            return (false, "Error in saving changes");
        }
        public async Task<(bool Success, LevelDTO dto, string message)> GetOneAsync(int Id)
        {
            var level = await _levelRepositiry.GetByIdAsync(Id);
            if (level == null)
            {
                return (false, null, "This ID not found.");
            }
            var dto = new LevelDTO { Id = level.Id, Name = level.Name, DepartmentId = level.DepartmentId };
            return (true, dto, "Retrieved Successfully");
        }
      

        public async Task<IEnumerable<LevelDTO>> GetAllAsync(int DepartmentID)
        {
            var levels = await _levelRepositiry.GetDepartmentsWithLevelAsync(DepartmentID);
            return levels.Select(l => new LevelDTO { Id = l.Id, Name = l.Name, DepartmentId = l.DepartmentId });
        }

        public async Task<IEnumerable<LevelWithSemesterDTO>> GetLevelWithSemesterAsync()
        {
            return await _levelRepositiry.GetLevelWithSemesterAsync();
        }
        
        public async Task<LevelWithSemesterDTO> GetLevelWithSemesterAsync(int levelId)
        {
            return await _levelRepositiry.GetLevelWithSemesterAsync(levelId);
        }



    }
}
