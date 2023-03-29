using languageSchoolAPI.Context;
using languageSchoolAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace languageSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly LanguageSchoolContext _context;

        public TeachersController(LanguageSchoolContext context)
        {
            _context = context;
        }

        [HttpGet("GetTeachers")]
        public async Task<ActionResult<IEnumerable<TeacherModel>>> GetTeachers()
        {
            return await _context.Teachers.ToListAsync();
        }

        [HttpGet("GetTeacher/{id}")]
        public async Task<ActionResult<TeacherModel>> GetTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return teacher;
        }

        
        [HttpPut("PutTeacher/{id}")]
        public async Task<IActionResult> PutTeacher(int id, TeacherModel teacher)
        {
            if (id != teacher.TeacherId)
            {
                return BadRequest();
            }

            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
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

        
        [HttpPost("PostTeacher")]
        public async Task<ActionResult<TeacherModel>> PostTeacher(TeacherModel teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeacher", new { id = teacher.TeacherId }, teacher);
        }

        [HttpDelete("DeleteTeacher/{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.TeacherId == id);
        }
    }
}
