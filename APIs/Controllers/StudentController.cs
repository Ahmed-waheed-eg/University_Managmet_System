using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(StudentServices _studentService) : ControllerBase
    {

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetAllStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            if (students.Any())
            {
                return Ok(students);
            }
            return NotFound("No students found.");
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            // Assuming you have a service to handle the deletion logic
            var (success, errorMessage) = await _studentService.DeleteStudentAsync(id);
            if (success)
            {
                return Ok("Student deleted successfully.");
            }
            return BadRequest(errorMessage);
        }
    }
}
