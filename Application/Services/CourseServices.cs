using System;
using Domain.Entities;
using Application.Interfaces;
using Application.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CourseServices(ICourseRepository courseRepository,IUnitOfWork unitOfWork)
    {
        public async Task<(bool Success, int id, string ErrorMessage)> CreateAsync(CourseDTO dto)
        {
            var exists = await courseRepository.GetByNameAsync(dto.Name);
            var existsCode = await courseRepository.GetByCodeAsync(dto.Code);
            if (exists != null|| existsCode!=null)
            {
                return (false, 0, "This Course already exists.");
            }
            var course = new Course {
                Name = dto.Name,
                Code=dto.Code,
                Hours = dto.Hours,
                Description = dto.Description,
            };
            await courseRepository.AddAsync(course);
            if (await unitOfWork.IsCompleteAsync())
            {
                return (true, course.Id, "Created Successfully");
            }
            return (false, 0, "Error in saving changes");
        }
        public async Task<(bool Success,  string ErrorMessage)> UpdateAsync(CourseDTO dto)
        {
            var course = await courseRepository.GetByIdAsync(dto.Id);
            if (course == null)
            {
                return (false,  "This ID not found.");
            }
            course.Name = dto.Name;
            course.Code = dto.Code;
            course.Hours = dto.Hours;
            course.Description = dto.Description;
            courseRepository.Update(course);
            if (await unitOfWork.IsCompleteAsync())
            {
                return (true, "Updated Successfully");
            }
            return (false,  "Error in saving changes");
        }
        public async Task<(bool Success, string ErrorMessage)> DeleteAsync(int Id)
        {
            var course = await courseRepository.GetByIdAsync(Id);
            if (course == null)
            {
                return (false, "This ID not found.");
            }
            courseRepository.Delete(course);
            if (await unitOfWork.IsCompleteAsync())
            {
                return (true, "Deleted Successfully.");
            }
            return (false, "Error in saving changes");
        }
        public async Task<IEnumerable<CourseDTO>> GetAllAsync()
        {
            var courses = await courseRepository.GetAllAsync();
            return courses.Select(c => new CourseDTO
            {
                Id = c.Id,
                Name = c.Name,
                Code = c.Code,
                Hours = c.Hours,
                Description = c.Description
            });
        }
        public async Task<CourseDTO> GetByIdAsync(int Id)
        {
            var course = await courseRepository.GetByIdAsync(Id);
            if (course == null)
            {
                return null; 
            }
            return new CourseDTO
            {
                Id = course.Id,
                Name = course.Name,
                Code = course.Code,
                Hours = course.Hours,
                Description = course.Description
            };
        }
        
        public async Task<PaginationDTO<CourseDTO>> GetAllAsync(int pageNumber, int pageSize)
        {
            var pagination = await courseRepository.GetAllAsync(pageNumber, pageSize);
            return new PaginationDTO<CourseDTO>
            {
                values = pagination.values.Select(c => new CourseDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Code = c.Code,
                    Hours = c.Hours,
                    Description = c.Description
                }),
                TotalCount = pagination.TotalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }
        public async Task<CourseDTO> GetByCodeAsync(string code)
        {
            var course = await courseRepository.GetByCodeAsync(code);
            if (course == null)
            {
                return null;
            }
            return new CourseDTO
            {
                Id = course.Id,
                Name = course.Name,
                Code = course.Code,
                Hours = course.Hours,
                Description = course.Description
            };
        }




}
}
