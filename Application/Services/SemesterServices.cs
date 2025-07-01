using Application.Interfaces;
using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Application.Services
{
    public class SemesterServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISemesterRepository _semesterRepository;
        private readonly IMapper _mapper;
        public SemesterServices(IUnitOfWork unitOfWork, ISemesterRepository semesterRepository,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _semesterRepository = semesterRepository;
            _mapper = mapper;

        }

        public async Task<(bool Success, int id, string ErrorMessage)> CreateAsync(SemesterDTO dto)
        {
            var exists = await _semesterRepository.AnyAsync(s => s.Name == dto.Name);
            if (exists)
            {
                return (false, 0, "This Semester already exists.");
            }
            if (dto.LevelId <= 0)
            {
                return (false, 0, "Invalid Level ID.");
            }
            var orderExists = await _semesterRepository.AnyAsync(s => s.Order == dto.order && s.LevelId == dto.LevelId);
            if (orderExists)
            {
                return (false, 0, "This order already exists for the specified Level.");
            }
            // Check if the Level exists
            var levelExists = await _semesterRepository.AnyAsync(l=>l.Id==dto.LevelId);
            if (!levelExists)
            {
                return (false, 0, "Level ID does not exist.");
            }
            var semester = new Semester { Name = dto.Name,Order=dto.order, LevelId = dto.LevelId };
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
            var dto = _mapper.Map<SemesterDTO>(semester);
            return (true, dto, "Retrieved Successfully");
        }


        public async Task<IEnumerable<SemesterDTO>> GetAllByLevelIdAsync(int levelId)
        {
            var semesters = await _semesterRepository.GetAllAsync(s=>s.LevelId==levelId);
            return semesters.Select(s => new SemesterDTO
            {
                Id = s.Id,
                Name = s.Name,
                LevelId = s.LevelId
            }).ToList();

        }


        public async Task<(bool Success,string Message)> SemestersCloseAsync(int SemesterOrder)
        {
            var semesters = await _semesterRepository.GetAllAsync(s => s.Order == SemesterOrder && s.IsActive);
            if (semesters == null || !semesters.Any())
            {
                return (false, "No active semesters found for the specified order.");
            }
            foreach (var semester in semesters)
            {
                semester.IsActive = false;
                _semesterRepository.Update(semester);
            }
            if (await _unitOfWork.IsCompleteAsync())
            {
                return (true, "Semesters closed successfully.");
            }
            return (false, "Error in closing semesters.");
        }

        public async Task<(bool Success,string message)> SemestersActiveAsync(int SemesterOrder)
        {
            var semesters = await _semesterRepository.GetAllAsync(s => s.Order == SemesterOrder && !s.IsActive);
            if (semesters == null || !semesters.Any())
            {
                return (false, "No inactive semesters found for the specified order.");
            }
            if (semesters.Any(s => s.IsActive))
            {
                return (false, "Some semesters are already active.");
            }

            foreach (var semester in semesters)
            {
                semester.IsActive = true;
                _semesterRepository.Update(semester);
            }
            if (await _unitOfWork.IsCompleteAsync())
            {
                return (true, "Semesters opened successfully.");
            }
            return (false, "Error in opening semesters.");
        }





    }
}
