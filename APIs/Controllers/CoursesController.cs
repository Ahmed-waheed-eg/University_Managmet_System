using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController(CourseServices courseServices) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetAll()
        {
            var courses = await courseServices.GetAllAsync();
            if (courses.Any())
            {
                return Ok(courses);
            }
            return NotFound("No courses found.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetById(int id)
        {
            var Course = await courseServices.GetByIdAsync(id);
            if (Course == null)
            {
                return NotFound("Course not found.");
            }
            return Ok(Course);
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var (success, id, message) = await courseServices.CreateAsync(dto);
            if (success)
            {
                return CreatedAtAction(nameof(GetById), new { id = id }, new { success = true, id = id, message = message });
            }
            return BadRequest(new { success = false, message = message });
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CourseDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var (success, message) = await courseServices.UpdateAsync(dto);
            if (success)
            {
                return Ok(new { success = true, message = message });
            }
            return BadRequest(new { success = false, message = message });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (success, message) = await courseServices.DeleteAsync(id);
            if (success)
            {
                return Ok(new { success = true, message = message });
            }
            return BadRequest(new { success = false, message = message });

        }
    }
}
