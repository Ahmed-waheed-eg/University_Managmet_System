using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DepartmentService
    {
        private readonly IDepartmentRepositiry _departmentRepositiry;
        private readonly ILevelRepositiry _levelRepository;
        private readonly ISemesterRepository _semesterRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IDepartmentRepositiry departmentRepositiry, IUnitOfWork unitOfWork, ILevelRepositiry levelRepository ,ISemesterRepository semesterRepository,IMapper mapper)
        {
            _departmentRepositiry = departmentRepositiry;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _levelRepository = levelRepository;
            _semesterRepository = semesterRepository;
        }

        public async Task<(bool Success,int id, string ErrorMessage)> CreateWithLevels(CreateDepartmentDTO dto)
        {
            var exists= await _departmentRepositiry.GetByAsync(d => d.Name == dto.Name);
            if (exists != null)
            {
                return (false, 0, "This Department alredy Exist.");
            }

            var depart = new Department { Name = dto.Name, Description = dto.Description };
            await _departmentRepositiry.AddAsync(depart);

            if (await _unitOfWork.IsCompleteAsync())
            {
                // Create levels for the department
                for (int i = 1; i <= dto.NumberOfLevels; i++)
                {
                var level = new Level
                {
                    Name = $"Level {i}",
                    order = i,
                    DepartmentId = depart.Id
                };
                  
                         await _CreateLevelsForDepartment(level);
                 }



                if (await _unitOfWork.IsCompleteAsync())
                {
                    return (true, depart.Id, "Crested Successed");
                }
            }

            return (false, 0, "Error in save setting");


        }


        public async Task<(bool Success,DepartmentWithAllDataDTO DTO, string ErrorMessage)> GetAllDepartmentDitails(int id)
        {
            var department = await _departmentRepositiry.GetByAsync(d => d.Id == id);
            if (department == null)
            {
                return (false, null, "This Department not found.");
            }
            var departmentWithAllData = await _departmentRepositiry.GetDepartmentWithLevelsAndSemestersAsync(id);
            if (departmentWithAllData != null)
            {
                var DTO=_mapper.Map<DepartmentWithAllDataDTO>(departmentWithAllData);
                return (true, DTO, "Department found successfully.");
            }

            return (false, null, "This Department not found.");
        }


        public async Task <(bool Success, string ErrorMessage)> DeleteAsync(int Id)
        {
            var de = await _departmentRepositiry.GetByIdAsync(Id);
            if (de == null)
            {
                return (false,"this ID not found."); 
            }
            _departmentRepositiry.Delete(de);
            if(await _unitOfWork.IsCompleteAsync())
            {
                return (true, "Deleted Successfully.");
            }
            return (false, "Error in save changes");
        }

        public async Task<(bool Success,DepartmentDTO dto,string message)>GetOne(int Id)
        {
            var de = await _departmentRepositiry.GetByIdAsync(Id);
            if (de != null)
            {
                return (true, new DepartmentDTO{ID=de.Id,Name=de.Name,Description=de.Description}, "this ID is found.");
            }
                return (false,null, "this ID not found.");
        }
        public async Task<(bool Success, DepartmentDTO dto, string message)> GetOne(string Name)
        {
            var de = await _departmentRepositiry.GetByAsync(d=>d.Name == Name);
            if (de != null)
            {
                return (true, new DepartmentDTO { ID = de.Id, Name = de.Name, Description = de.Description }, "this ID is found.");
            }
            return (false, null, "this Name not found.");
        }

        public async Task<(bool Success,string message)> UpdateAsync(DepartmentDTO dto)
        {
            var EntityToUpdate = await _departmentRepositiry.GetByIdAsync(dto.ID);
            if (EntityToUpdate == null)
            {
                return( false,$"There is No Department with this ID ( {dto.ID} ).");
            }
            bool NameExists = await _departmentRepositiry.AnyAsync(x => x.Name == dto.Name );
            if(NameExists)
            {
                return (false, "This Department Name already exists.");
            }

            EntityToUpdate.Name=dto.Name;
            EntityToUpdate.Description=dto.Description;
            _departmentRepositiry.Update(EntityToUpdate);
            if(await _unitOfWork.IsCompleteAsync())
            return (true, "Updated Successfully.");

            return (false, "Error in save changes.");
        }

        public async Task<IEnumerable<DepartmentDTO>>GetAll()
        {

            var departments = await _departmentRepositiry.GetAllAsync();
            return departments.Select(d => new DepartmentDTO
            {
                ID = d.Id,
                Name = d.Name,
                Description = d.Description
            });

        }
















        private async Task _CreateLevelsForDepartment(Level level)
        {
            await _levelRepository.AddAsync(level);
            if (await _unitOfWork.IsCompleteAsync())
            {
                await _CreateSemetersForLevel(level);
            }
          

        }

        private async Task _CreateSemetersForLevel(Level level)
        {
           for (int i = 1; i <= 2; i++)
            {
                var semester = new Semester
                {
                    Name = $"Semester {i},Level {level.order}",
                    Order = i,
                    LevelId = level.Id
                };
                await _semesterRepository.AddAsync(semester);
            }

            await _CreatSummerSemesterForLevel(level);

        }
        
        private async Task _CreatSummerSemesterForLevel(Level level)
        {
            var summerSemester = new Semester
            {
                Name = $"Summer Semester,Level{level.order}",
                Order = 3, //  summer semester is the third semester
                LevelId = level.Id
            };
            await _semesterRepository.AddAsync(summerSemester);
        }

    }
}
