using Application.DTOs;
using Application.DTOs.DepartmentsDTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace APIs.Controllers
{
    [Route("api/Department")]
   // [Authorize(Roles = "SuperAdmin")]
    [ApiController]
    public class DepartmentCotroller : ControllerBase
    {
        private readonly DepartmentService _departmentService;
        public DepartmentCotroller(DepartmentService departmentService)
        {
            _departmentService = departmentService;
           
        }


        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

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




        [HttpPost("CreateWitLevels/")]
        public async Task<IActionResult> Create([FromForm] CreateDepartmentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (success,Id,Message)=await _departmentService.CreateWithLevels(dto);

            if (success) 
            {
                return Ok(new {success=true,id=Id,message=Message});
            }

            return Conflict(new {success=false,message=Message});
        }


        [HttpGet("GetWithAllData/{Id}")]
        public async Task<ActionResult<DepartmentWithAllDataDTO>> GetWithAllData(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest(new { success = false, message = "Invalid Department ID." });
            }
            var (success, dto, message) = await _departmentService.GetAllDepartmentDitails(Id);
            if (success)
            {
                return Ok(dto);
            }
            return BadRequest(new { success = false, message = message });
        }





        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] DepartmentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var (success, message) = await _departmentService.UpdateAsync(dto);
            if (success)
            {
                return Ok(new { success = true, Message = message });
            }
            return Conflict(new { success = false, Message = message });
        }


        [HttpDelete("Delete")]
        public async Task<IActionResult> delete(int id)
        {
            var (success,message)=await _departmentService.DeleteAsync(id);
            if (success)
            {
                return Ok(new {success=true,Message =message});
            }
            return Conflict(new {success=false, Message =message});
        }
    }
}
