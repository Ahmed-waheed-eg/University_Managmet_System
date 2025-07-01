using Microsoft.AspNetCore.Http;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemeterEnrollConteroller(TermRecordServices _termRecordServices) : ControllerBase
    {
        [HttpGet]
        [Route("GetTermRecord/{id}")]
        public async Task<IActionResult> GetTermRecord(int id)
        {
            var termRecord = await _termRecordServices.GetTermRecordAsync(id);
            if (termRecord == null)
            {
                return NotFound();
            }
            return Ok(termRecord);
        }


        [HttpPut("{studentId}")]
        public async Task<IActionResult> AssignStudentToCurrentSemester(int studentId)
        {
            var result = await _termRecordServices.AssignsStudentInSemester(studentId);
            if (!result.Success)
            {
                return BadRequest(result.message);
            }
            return Ok(result.DTO);
        }

    }
}
