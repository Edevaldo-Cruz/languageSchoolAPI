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
        private readonly LogEntryController _logEntryController;

        public TeachersController(LanguageSchoolContext context, LogEntryController logEntryController)
        {
            _context = context;
            _logEntryController = logEntryController;
        }

        [HttpPost("CreateTeacher")]
        public async Task<IActionResult> CreateTeacher(TeacherModel teacher)
        {
            var validationError = await ValidateTeacher(teacher);
            if (validationError != null)
            {
                return validationError;
            }

            try
            {
                _context.Teachers.Add(teacher);
                await _context.SaveChangesAsync();

                string descripton = "Incluindo novo registro do professor " + teacher.Name;
                await _logEntryController.CreateLogEntry(descripton, "Novo registro professor");
            }
            catch (DbUpdateException ex)
            {
                string descripton = "Erro ao tentar gravar o registro do professor " + teacher.Name + ". Erro: " + ex;
                await _logEntryController.CreateLogEntry(descripton, "Erro novo registro professor");
                return BadRequest(ex);
            }
            return CreatedAtAction("GetTeacher", new { id = teacher.TeacherId }, teacher);
        }


        [HttpPut("EditTeacher/{id}")]
        public async Task<IActionResult> EditTeacher(int id, TeacherModel teacher)
        {
            var teacherBank = await _context.Teachers.FindAsync(id);
            var validationError = await ValidateTeacher(teacher, id);

            if (teacherBank == null)
                return BadRequest("Professor não encontrado.");

            if (validationError != null)
            {
                return validationError;
            }

            try
            {
                teacherBank.Name = teacher.Name;
                teacherBank.CPF = teacher.CPF;
                teacherBank.Phone = teacher.Phone;
                teacherBank.Address = teacher.Address;
                teacherBank.Email = teacher.Email;
                teacherBank.Birthdate = teacher.Birthdate;
                teacherBank.GenderId = teacher.GenderId;
                teacherBank.Nationality = teacher.Nationality;
                teacherBank.Observation = teacher.Observation;

                _context.Teachers.Update(teacherBank);
                await _context.SaveChangesAsync();

                string descripton = "Alterando registro do professor " + teacherBank.Name;
                await _logEntryController.CreateLogEntry(descripton, "Alteração de registro professor");
                return Ok(teacherBank);
            }
            catch (Exception ex)
            {

                string descripton = "Erro ao tentar alterar o registro do professor " + teacher.Name + ". Erro: " + ex;
                await _logEntryController.CreateLogEntry(descripton, "Erro alterar registro aluno");
                return StatusCode(422, $"Ocorreu um erro ao atualizar o estudante: {ex.Message}");
            }

        }

        [HttpGet("GetAllTeachers")]
        public async Task<ActionResult<IEnumerable<TeacherModel>>> GetTeachers()
        {
            return await _context.Teachers.ToListAsync();
        }

        [HttpGet("GetTeacherById/{id}")]
        public async Task<ActionResult<TeacherModel>> GetTeacherById(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return teacher;
        }


        [HttpGet("GetTeacherByName/{name}")]
        public async Task<ActionResult<IEnumerable<TeacherModel>>> GetTeacherByName(string name)
        {
            var teacher = await _context.Teachers.Where(s => s.Name.Contains(name)).ToListAsync();
            if (teacher == null || teacher.Count == 0)
            {
                return NotFound("Professor não encontrado.");
            }
            return teacher;
        }

        [HttpDelete("DeleteTeacher/{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound("Professor não encontrado.");
            }

            try
            {
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
                string descripton = "Excluindo registro do professor " + teacher.Name;
                await _logEntryController.CreateLogEntry(descripton, "Exclusão professor");
                return NoContent();
            }
            catch
            {
                string descripton = "Erro ao excluindo registro do aluno " + teacher.Name;
                await _logEntryController.CreateLogEntry(descripton, "Erro de exclusão professor");
                return BadRequest("Erro. Não foi possivel exluir o registro.");
            }
        }




        private async Task<IActionResult> ValidateTeacher(TeacherModel teacher, int? teacherId = null)
        {
            var existingTeacher = await _context.Teachers.FirstOrDefaultAsync(s => s.CPF == teacher.CPF && (!teacherId.HasValue || s.TeacherId != teacherId.Value));

            if (existingTeacher != null)
            {
                string descripton = "Erro ao tentar gravar o registro do professor " + teacher.Name + ". CPF duplicado.";
                await _logEntryController.CreateLogEntry(descripton, "Erro novo registro");
                return BadRequest("Já existe um registro com esse CPF.");
            }

            List<int> genderIds = new List<int> { 1, 2, 3 };

            if (!genderIds.Contains(teacher.GenderId))
            {
                string descripton = "Erro ao tentar gravar o registro do professor " + teacher.Name + ". Gênero inválido.";
                await _logEntryController.CreateLogEntry(descripton, "Erro novo registro");
                return BadRequest("Gênero inválido.");
            }

            return null;
        }
    }
}
