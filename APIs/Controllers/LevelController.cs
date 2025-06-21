using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/Level")]
    [ApiController]
    public class LevelController : ControllerBase
    {
        private readonly LevelService _levelService;
        public LevelController(LevelService levelService)
        {
            _levelService = levelService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<LevelWithSemesterDTO>>> GetAll()
        {
            var levels = await _levelService.GetLevelWithSemesterAsync();
            if (levels.Any())
            {
                return Ok(levels);
            }
            return NotFound("No levels found.");
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<LevelWithSemesterDTO>> GetById(int id)
        {
            var (success, level, message) = await _levelService.GetOneAsync(id);
            if (success)
            {
                return Ok(level);
            }
            return NotFound(new { success = false, message = message });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LevelDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var (success, id, message) = await _levelService.CreateAsync(dto);
            if (success)
            {
                return CreatedAtAction(nameof(GetById), new { id = id }, new { success = true, id = id, message = message });
            }
            return BadRequest(new { success = false, message = message });
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] LevelDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var (success, message) = await _levelService.UpdateAsync(dto);
            if (success)
            {
                return Ok(new { success = true, message = message });
            }
            return BadRequest(new { success = false, message = message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (success, message) = await _levelService.DeleteAsync(id);
            if (success)
            {
                return Ok(new { success = true, message = message });
            }
            return NotFound(new { success = false, message = message });

        }

        [HttpGet("GetAllByDepartmentId/{departmentId}")]
        public async Task<ActionResult<IEnumerable<LevelDTO>>> GetAllByDepartmentId(int departmentId)
        {
            var levels = await _levelService.GetAllAsync(departmentId);
            if (levels.Any())
            {
                return Ok(levels);
            }
            return NotFound("No levels found for this department.");
        }
    }
}
