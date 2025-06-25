using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LoginServices _loginServices;
        public AuthController(LoginServices loginServices)
        {
            _loginServices = loginServices;
        }

        [HttpPost("login/SuperAdmin")]
        public async Task<IActionResult> LoginSuperAdminAsync([FromBody] LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid login request.");
            }
            var response = await _loginServices.LoginSuperAdminAsync(loginDto.Email, loginDto.Password);
            if (response == null)
            {
                return Unauthorized("Invalid email or password.");
            }
            return Ok(response);
        }


        [HttpPost("login/Admin")]
        public async Task<IActionResult> LoginAdminAsync([FromBody] LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid login request.");
            }
            var response = await _loginServices.LoginAdminAsync(loginDto.Email, loginDto.Password);
            if (response == null)
            {
                return Unauthorized("Invalid email or password.");
            }
            return Ok(response);
        }
    }
}
