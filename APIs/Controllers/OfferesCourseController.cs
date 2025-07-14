using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferesCourseController(OfferedCousreServices _offeredCousreServices) : ControllerBase
    {

        [HttpGet("Pagination/PageNumber({PageNumber}),PageSize({PageSize})")]
        public async Task<ActionResult<PaginationDTO<OfferedCoursesDTO>>> GetAll(int PageNumber = 1, int PageSize = 10)
        {
            var pagination = await _offeredCousreServices.GetAllAsync(PageNumber, PageSize);
            if (pagination.values.Any())
            {
                return Ok(pagination);
            }
            return NotFound("No offered courses found.");
        }



        [HttpPost]
        public async Task<IActionResult> Create(int CourseId, int SemesterID)
        {
            if (CourseId <= 0 || SemesterID <= 0)
            {
                return BadRequest("Invalid CourseId or SemesterID.");
            }

            var (success, id, message) = await _offeredCousreServices.CreateAsync(CourseId, SemesterID);
            if (success)
            {
                return CreatedAtAction(nameof(GetById), new { id = id }, new { success = true, id = id, message = message });
            }
            return BadRequest(new { success = false, message = message });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var offeredCourse = await _offeredCousreServices.GetByIdAsync(id);
            if (offeredCourse == null)
            {
                return NotFound("Offered Course not found.");
            }
            return Ok(offeredCourse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (success, message) = await _offeredCousreServices.DeleteAsync(id);
            if (success)
            {
                return Ok(new { success = true, message = message });
            }
            return NotFound(new { success = false, message = message });
        }



        [HttpGet("SemesterID/{id}")]
        public async Task<IActionResult> GetBySemesterId(int id)
        {
            var offeredCourse = await _offeredCousreServices.GetAllPerSemesterAsync(id);
            if (!offeredCourse.Any())
            {
                return NotFound("Offered Course not found.");
            }
            return Ok(offeredCourse);
        }

        [HttpGet("LevelID/{id}")]
        public async Task<IActionResult> GetByLevelId(int id)
        {
            var offeredCourse = await _offeredCousreServices.GetAllPerLevelAsync(id);
            if (!offeredCourse.Any())
            {
                return NotFound("Offered Course not found.");
            }
            return Ok(offeredCourse);
        }

        [HttpGet("DepartmentID/{id}")]
        public async Task<IActionResult> GetByDepartmentId(int id)
        {
            var offeredCourse = await _offeredCousreServices.GetAllperDepartmentAsync(id);
            if (!offeredCourse.Any())
            {
                return NotFound("Offered Course not found.");
            }
            return Ok(offeredCourse);
        }


        [HttpGet("StudentID/{id}")]
        public async Task<IActionResult> GetByStudentId(int id)
        {
            var offeredCourse = await _offeredCousreServices.GetAllPerStudentIDAsync(id);

            if (!offeredCourse.Success)
            {
                return NotFound(offeredCourse.message);
            }
            return Ok(offeredCourse.Item2);


        }




    }
}
