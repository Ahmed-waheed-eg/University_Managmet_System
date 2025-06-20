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
            var exists= await _departmentRepositiry.GetByName(dto.Name);
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
            var de = await _departmentRepositiry.GetByName(Name);
            if (de != null)
            {
                return (true, new DepartmentDTO { ID = de.Id, Name = de.Name, Description = de.Description }, "this ID is found.");
            }
            return (false, null, "this Name not found.");
        }


        public async Task<IEnumerable<Department>>GetAll()
        {
            return await _departmentRepositiry.GetAllAsync();
           
        }

    }
}
