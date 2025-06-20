using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace APIs.Controllers
{
    [Route("api/Department")]
    [ApiController]
    public class DepartmentCotroller : ControllerBase
    {
        private readonly DepartmentService _departmentService;
        public DepartmentCotroller(DepartmentService departmentService)
        {
            _departmentService = departmentService;
           
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<DepartmentDTO>>GetAll()
        {
            var Dto=await _departmentService.GetAll();
            if (Dto.Count()>0) 
            return Ok(Dto);

            return BadRequest("Not Found");
        }



        [HttpGet("GetById/{Id}")]
        public async Task<ActionResult<DepartmentDTO>>GetById(int Id)
        {
            var (success,DTO,message)=await _departmentService.GetOne(Id);
            if (success)
            {
                return Ok(DTO);
            }
            return BadRequest(new {success=false,message=message});
        }

        [HttpGet("GetByName/{Name}")]
        public async Task<ActionResult<DepartmentDTO>> GetByName(string Name)
        {
            var (success, DTO, message) = await _departmentService.GetOne(Name);
            if (success)
            {
                return Ok(DTO);
            }
            return BadRequest(new { success = false, message = message });
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (success,id,message)=await _departmentService.Create(dto);

            if (success) 
            {
                return Ok(new {success=true,id=id,message=message});
            }

            return Conflict(new {success=false,message=message});
        }


        [HttpDelete]
        public async Task<IActionResult> delete(int id)
        {
            var (success,message)=await _departmentService.DeleteAsync(id);
            if (success)
            {
                return Ok(new {success=true,message=message});
            }
            return Conflict(new {success=false, message=message});
        }
    }
}
