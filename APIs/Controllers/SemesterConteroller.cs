using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/Semester")]
    [ApiController]
    public class SemesterConteroller : ControllerBase
    {
        private readonly SemesterServices _semesterServices;
        public SemesterConteroller(SemesterServices semesterServices)
        {
            _semesterServices = semesterServices;
        }


        
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<SemesterDTO>> GetById(int id)
        {
            var (success, semester, message) = await _semesterServices.GetOneAsync(id);
            if (success)
            {
                return Ok(semester);
            }
            return NotFound(new { success = false, message = message });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SemesterDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var (success, id, message) = await _semesterServices.CreateAsync(dto);
            if (success)
            {
                return CreatedAtAction(nameof(GetById), new { id = id }, new { success = true, id = id, message = message });
            }
            return BadRequest(new { success = false, message = message });
        }

     

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID.");
            }
            var (success, message) = await _semesterServices.DeleteAsync(id);
            if (success)
            {
                return Ok(new { success = true, message = message });
            }
            return NotFound(new { success = false, message = message });
        }

  
        [HttpGet("GetAllByLevelId/{levelId}")]
        public async Task<ActionResult<IEnumerable<SemesterDTO>>> GetAllByLevelIdAsync(int levelId)
        {
            if (levelId <= 0)
            {
                return BadRequest("Invalid level ID.");
            }
            var semesters = await _semesterServices.GetAllByLevelIdAsync(levelId);
            if (semesters.Any())
            {
                return Ok(semesters);
            }
            return NotFound("No semesters found for this level.");
        }



        [HttpPost("Semesters/{semestersOrder}/open")]
        public async Task<IActionResult> ActivateSemester(int semestersOrder)
        {
            if (semestersOrder <= 0&& semestersOrder>3)
            {
                return BadRequest("Invalid semester order.");
            }
            var (success,Message) = await _semesterServices.SemestersActiveAsync(semestersOrder);
            if (success)
            {
                return Ok(new { success = true, message = Message });
            }
            return NotFound(new { success = false, message = "Semester not found or activation failed." });
        }

        [HttpPost("Semesters/{semestersOrder}/close")]
        public async Task<IActionResult> CloseSemester(int semestersOrder)
        {
            if (semestersOrder <= 0 && semestersOrder > 3)
            {
                return BadRequest("Invalid semester order.");
            }
            var (success, Message) = await _semesterServices.SemestersCloseAsync(semestersOrder);
            if (success)
            {
                return Ok(new { success = true, message = Message });
            }
            return NotFound(new { success = false, message = "Semester not found or closure failed." });
        }


    }

}
