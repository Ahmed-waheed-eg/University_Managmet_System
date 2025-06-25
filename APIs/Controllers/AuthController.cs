using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("login")]
        public async Task<IActionResult> LoginSuperAdminAsync([FromBody] LoginDTO loginDto)
        {
            if (loginDto.Email=="Ahmedd")
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
    }
}
