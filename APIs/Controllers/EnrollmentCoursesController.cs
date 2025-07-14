using Application.Services;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentCoursesController(EnrollmentServices _enrollmentServices) : ControllerBase
    {

        [HttpPut("Enroll/StudentId{StudentId}/OfferedCourseId{OfferdCourseId}")]
        public async Task<IActionResult> Create(int StudentId, int OfferdCourseId)
        {
            if (StudentId <= 0 || OfferdCourseId <= 0) { return BadRequest("this id invalide."); }
            var Items = await _enrollmentServices.EnrollStudentInCourse(StudentId, OfferdCourseId);
            if (!Items.Success)
            {
                return BadRequest(Items.message);
            }

            return Ok(new { Success = Items.Success, EnrollId = Items.EnrollmentId, message = Items.message });
        }
    }
}
