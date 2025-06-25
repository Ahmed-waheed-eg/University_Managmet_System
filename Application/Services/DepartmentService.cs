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
    public class DepartmentService
    {
        private readonly IDepartmentRepositiry _departmentRepositiry;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IDepartmentRepositiry departmentRepositiry, IUnitOfWork unitOfWork)
        {
            _departmentRepositiry = departmentRepositiry;
            _unitOfWork = unitOfWork;
        }

        public async Task<(bool Success,int id, string ErrorMessage)> Create(DepartmentDTO dto)
        {
            var exists= await _departmentRepositiry.GetByNameAsync(dto.Name);
            if (exists != null)
            {
                return (false, 0, "This Department alredy Exist.");
            }

            var depart = new Department { Name = dto.Name, Description = dto.Description };
            await _departmentRepositiry.AddAsync(depart); 
            
            if(await _unitOfWork.IsCompleteAsync())
            {
                return (true, depart.Id, "Crested Successed");
            }

            return (false, 0, "Error in save setting");


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
            var de = await _departmentRepositiry.GetByNameAsync(Name);
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

    }
}
