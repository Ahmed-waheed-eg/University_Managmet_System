using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;
using System.Net;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateUseresController(CreateUseresServices _createUseresServices) : ControllerBase
    {

        [HttpPost("CreateStudent")]
        public async Task<IActionResult> CreateStudentAsync([FromBody] CreateStudentDTO student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid student data.");
            }
            var result = await _createUseresServices.CreateStudent(student);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(new { Id = result.id, Message = result.ErrorMessage });
        }


        [HttpPost("CreateProfessor")]
        public async Task<IActionResult> CreateProfessorAsync([FromBody] CreateProfessorDTO professor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid professor data.");
            }
            var result = await _createUseresServices.CreateProfessor(professor);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(new { Id = result.id, Message = result.ErrorMessage });
        }

    }
}
