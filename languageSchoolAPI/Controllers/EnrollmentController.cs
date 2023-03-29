using languageSchoolAPI.Context;
using languageSchoolAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace languageSchoolAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly LanguageSchoolContext _context;

        public EnrollmentController(LanguageSchoolContext context)
        {
            _context = context;
        }

        [HttpGet("GetEnrollments")]
        public async Task<ActionResult<IEnumerable<EnrollmentModel>>> GetEnrollments()
        {
            return await _context.Enrollments.ToListAsync();
        }

        [HttpGet("GetEnrollment/{id}")]
        public async Task<ActionResult<EnrollmentModel>> GetEnrollment(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);

            if (enrollment == null)
            {
                return NotFound();
            }

            return enrollment;
        }

        [HttpPost("PostEnrollment")]
        public async Task<ActionResult<EnrollmentModel>> PostEnrollment(EnrollmentModel enrollment)
        {
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEnrollment), new { id = enrollment.EnrollmentId }, enrollment);
        }

        [HttpPut("PutEnrollment/{id}")]
        public async Task<IActionResult> PutEnrollment(int id, EnrollmentModel enrollment)
        {
            if (id != enrollment.EnrollmentId)
            {
                return BadRequest();
            }

            _context.Entry(enrollment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("DeleteEnrollment/{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.EnrollmentId == id);
        }
    }
}
