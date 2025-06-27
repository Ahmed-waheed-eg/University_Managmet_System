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


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<SemesterDTO>>> GetAll()
        {
            var semesters = await _semesterServices.GetAllAsync();
            if (semesters.Any())
            {
                return Ok(semesters);
            }
            return NotFound("No semesters found.");
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

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SemesterDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var (success,  message) = await _semesterServices.UpdateAsync(dto);
            if (success)
            {
                return Ok(new { success = true, message = message });
            }
            return BadRequest(new { success = false, message = message });
        }

        [HttpDelete("{id}")]
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



        [HttpPost("Activate/{semesterId}")]
        public async Task<IActionResult> ActivateSemester(int semesterId)
        {
            if (semesterId <= 0)
            {
                return BadRequest("Invalid semester ID.");
            }
            var success = await _semesterServices.ActiveSemesterAsync(semesterId);
            if (success)
            {
                return Ok(new { success = true, message = "Semester activated successfully." });
            }
            return NotFound(new { success = false, message = "Semester not found or activation failed." });
        }


    }

}
