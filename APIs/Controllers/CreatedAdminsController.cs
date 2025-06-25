using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatedAdminsController : ControllerBase
    {
        private readonly CreatedUseresServices _createdUseresServices;
        public CreatedAdminsController(CreatedUseresServices createdUseresServices)
        {
            _createdUseresServices = createdUseresServices;
        }

        [HttpPost("CreateSuperAdmin")]
        public async Task<IActionResult> CreateSuperAdminAsync([FromBody] Application.DTOs.CreatesdUsersDTO superAdmin)
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
    }
}
