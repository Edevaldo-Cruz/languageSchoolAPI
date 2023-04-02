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
        private readonly LogEntryController _logEntryController;

        public EnrollmentController(LanguageSchoolContext context, LogEntryController logEntryController)
        {
            _context = context;
            _logEntryController = logEntryController;
        }

        [HttpPost("CreateEnrollment")]
        public async Task<ActionResult<EnrollmentModel>> CreateEnrollment(int StudentId, int ClassroomId)
        {
            var validationResult = ValidateStudentsAndClassrooms(StudentId, ClassroomId);
            if (validationResult != null)
                return validationResult;

            bool enrollmentExists = await _context.Enrollments.AnyAsync(e => e.StudentId == StudentId && e.ClassroomId == ClassroomId);
            if (enrollmentExists)
            {
                string descripton = "O aluno já está matriculado nesta turma.";
                await _logEntryController.CreateLogEntry(descripton, "Erro nova matricula");
                return BadRequest(descripton);
            }

            int enrollmentCount = _context.Enrollments.Count(e => e.ClassroomId == ClassroomId);
            if (enrollmentCount >= 5)
            {
                string descripton = "Turma atingiu o limite maximo de aluno matriculado.";
                await _logEntryController.CreateLogEntry(descripton, "Erro nova matricula");
                return BadRequest("A turma já tem o número máximo de matrículas permitido.");
            }

            try
            {
                EnrollmentModel enrollment = new EnrollmentModel();
                enrollment.StudentId = StudentId;
                enrollment.ClassroomId = ClassroomId;
                enrollment.EnrollmentDate = DateTime.Now;

                _context.Enrollments.Add(enrollment);
                await _context.SaveChangesAsync();

                string descripton = "Incluindo nova matricula.";
                await _logEntryController.CreateLogEntry(descripton, "Nova matricula");
                return Ok(enrollment);
            }
            catch (DbUpdateException ex)
            {
                string descripton = "Erro ao tentar gravar a matricula. Erro: " + ex;
                await _logEntryController.CreateLogEntry(descripton, "Erro nova matricula");
                return BadRequest(ex);
            }

        }

        [HttpPut("EditEnrollment/{id}")]
        public async Task<ActionResult<EnrollmentModel>> EditEnrollment(int id, int StudentId, int ClassroomId, DateTime enrollmentDate)
        {
            var validationResult = ValidateStudentsAndClassrooms(StudentId, ClassroomId);
            if (validationResult != null)
                return validationResult;

            var enrollmentBank = _context.Enrollments.Find(id);
            if (enrollmentBank == null)
                return NotFound("Matricula não encontrada.");

            try
            {
                enrollmentBank.StudentId = StudentId != 0 ? StudentId : enrollmentBank.StudentId;
                enrollmentBank.ClassroomId = ClassroomId != 0 ? ClassroomId : enrollmentBank.ClassroomId;
                enrollmentBank.EnrollmentDate = enrollmentDate != DateTime.MinValue ? enrollmentDate : enrollmentBank.EnrollmentDate;

                _context.Enrollments.Update(enrollmentBank);
                await _context.SaveChangesAsync();

                string descripton = "Alterando registro da matricula " + enrollmentBank.EnrollmentId;
                await _logEntryController.CreateLogEntry(descripton, "Alteração de registro aluno");
                return Ok(enrollmentBank);
            }
            catch (Exception ex)
            {
                string descripton = "Erro ao tentar alterar o registro da matricula " + enrollmentBank.EnrollmentId + ". Erro: " + ex;
                await _logEntryController.CreateLogEntry(descripton, "Erro alterar matricula");
                return StatusCode(422, "Ocorreu um erro ao atualizar da matricula: " + ex.Message);
            }

        }

        [HttpGet("GetAllEnrollments")]
        public async Task<ActionResult<IEnumerable<EnrollmentModel>>> GetAllEnrollments()
        {
            return await _context.Enrollments.ToListAsync();
        }

        [HttpGet("GetEnrollmentById/{id}")]
        public async Task<ActionResult<EnrollmentModel>> GetEnrollmentById(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);

            if (enrollment == null)
            {
                return NotFound();
            }

            return enrollment;
        }

        [HttpDelete("DeleteEnrollment/{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound("Matricula não encontrada");
            }

            try
            {
                _context.Enrollments.Remove(enrollment);
                await _context.SaveChangesAsync();

                string descripton = "Excluindo a matricula " + enrollment.EnrollmentId;
                await _logEntryController.CreateLogEntry(descripton, "Exclusão da matricula");
                return NoContent();
            }
            catch
            {
                string descripton = "Erro ao excluir a matricula " + enrollment.EnrollmentId;
                await _logEntryController.CreateLogEntry(descripton, "Erro de exclusão matricula");
                return BadRequest("Erro. Não foi possivel exluir a matricula.");
            }

        }

        private ActionResult<EnrollmentModel> ValidateStudentsAndClassrooms(int StudentId, int ClassroomId)
        {
            var validStudent = _context.Students.Any(x => x.StudentId == StudentId);
            if (!validStudent)
            {
                return BadRequest("Aluno invalido");
            }

            var validClassrooms = _context.Classrooms.Any(x => x.ClassroomId == ClassroomId);
            if (!validClassrooms)
            {
                return BadRequest("Turma invalida");
            }

            return null;
        }
    }
}
