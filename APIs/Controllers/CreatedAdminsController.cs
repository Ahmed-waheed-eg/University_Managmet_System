using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Application.DTOs;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "SuperAdmin")]
    [ApiController]
    
    public class CreatedAdminsController(CreatedAdminsServices _createdUseresServices ) : ControllerBase
    {
       

        [HttpPost("CreateSuperAdmin")]
        public async Task<IActionResult> CreateSuperAdminAsync([FromForm] CreatesdUsersDTO superAdmin)
        {
            if (superAdmin == null || string.IsNullOrEmpty(superAdmin.Name) || string.IsNullOrEmpty(superAdmin.Email) || string.IsNullOrEmpty(superAdmin.Password))
            {
                return BadRequest("Invalid user creation request.");
            }
            var result = await _createdUseresServices.CreateSuperAdmin(superAdmin);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(new { Id = result.id, Message = result.ErrorMessage });
        }

        [HttpPost("CreateAdmin")]
        public async Task<IActionResult> CreateAdminAsync([FromForm] CreatesdUsersDTO admin)
        {
            if (admin == null || string.IsNullOrEmpty(admin.Name) || string.IsNullOrEmpty(admin.Email) || string.IsNullOrEmpty(admin.Password))
            {
                return BadRequest("Invalid user creation request.");
            }
            var result = await _createdUseresServices.CreateAdmin(admin);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(new { Id = result.id, Message = result.ErrorMessage });
           
        }


    }
}
