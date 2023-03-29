using languageSchoolAPI.Context;
using languageSchoolAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace languageSchoolAPI.Controllers
{
    [ApiController]
    [Route("Student")]
    public class StudentsController : ControllerBase
    {
        private readonly LanguageSchoolContext _context;
        private readonly LogEntryController _logEntryController;
        private readonly EnrollmentController _enrollmentController;

        public StudentsController(LanguageSchoolContext context, LogEntryController logEntryController, EnrollmentController enrollmentController)
        {
            _context = context;
            _logEntryController = logEntryController;
            _enrollmentController = enrollmentController;
        }

        //Continuar aqui add validação matricula automatica

        [HttpPost("CreateStudent")]
        public async Task<IActionResult> CreateStudent(StudentModel student)
        {
            var validationError = await ValidateStudent(student);
            if (validationError != null)
            {
                return validationError;
            }

            try
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                if (student.English)
                {
                    EnrollmentModel enrollment = new EnrollmentModel();
                    enrollment.StudentId = student.StudentId;
                    enrollment.EnrollmentDate = DateTime.Now;

                    switch (student.ProficiencyLevelEnglish)
                    {
                        case 1:
                            enrollment.ClassroomId = 1;
                            break;
                        case 2:
                            enrollment.ClassroomId = 2;
                            break;
                        case 3:
                            enrollment.ClassroomId = 3;
                            break;
                        default:
                            enrollment.ClassroomId = 1;
                            break;

                    }
                    await _enrollmentController.PostEnrollment(enrollment);
                }

                if (student.Spanish)
                {
                    EnrollmentModel enrollment = new EnrollmentModel();
                    enrollment.StudentId = student.StudentId;
                    enrollment.EnrollmentDate = DateTime.Now;

                    switch (student.ProficiencyLevelEnglish)
                    {
                        case 1:
                            enrollment.ClassroomId = 4;
                            break;
                        case 2:
                            enrollment.ClassroomId = 5;
                            break;
                        case 3:
                            enrollment.ClassroomId = 6;
                            break;
                        default:
                            enrollment.ClassroomId = 4;
                            break;

                    }
                    await _enrollmentController.PostEnrollment(enrollment);
                }

                if (student.French)
                {
                    EnrollmentModel enrollment = new EnrollmentModel();
                    enrollment.StudentId = student.StudentId;
                    enrollment.EnrollmentDate = DateTime.Now;

                    switch (student.ProficiencyLevelEnglish)
                    {
                        case 1:
                            enrollment.ClassroomId = 7;
                            break;
                        case 2:
                            enrollment.ClassroomId = 8;
                            break;
                        case 3:
                            enrollment.ClassroomId = 9;
                            break;
                        default:
                            enrollment.ClassroomId = 7;
                            break;

                    }
                    await _enrollmentController.PostEnrollment(enrollment);
                }

                string descripton = "Incluindo novo registro do aluno " + student.Name;
                await _logEntryController.CreateLogEntry(descripton, "Novo registro");
                return Ok(student);
            }
            catch (DbUpdateException ex)
            {
                string descripton = "Erro ao tentar gravar o registro do aluno " + student.Name + ". Erro: " + ex;
                await _logEntryController.CreateLogEntry(descripton, "Erro novo registro");
                return BadRequest(ex);
            }
        }

        [HttpPut("UpdateStudent/{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentModel student)
        {
            var studentBank = await _context.Students.FindAsync(id);
            var validationError = await ValidateStudent(student, id);
            if (studentBank == null)
                return BadRequest("Estudante não encontrado.");

            if (validationError != null)
            {
                return validationError;
            }

            try
            {
                studentBank.Name = student.Name;
                studentBank.CPF = student.CPF;
                studentBank.Phone = student.Phone;
                studentBank.Address = student.Address;
                studentBank.Email = student.Email;
                studentBank.Birthdate = student.Birthdate;
                studentBank.GenderId = student.GenderId;
                studentBank.Nationality = student.Nationality;
                studentBank.English = student.English;
                studentBank.ProficiencyLevelEnglish = student.ProficiencyLevelEnglish;
                studentBank.Spanish = student.Spanish;
                studentBank.ProficiencyLevelSpanish = student.ProficiencyLevelSpanish;
                studentBank.French = student.French;
                studentBank.ProficiencyLevelFrench = student.ProficiencyLevelFrench;
                studentBank.Observation = student.Observation;

                _context.Students.Update(studentBank);
                await _context.SaveChangesAsync();

                string descripton = "Alterando registro do aluno " + studentBank.Name;
                await _logEntryController.CreateLogEntry(descripton, "Alteração de registro");
                return Ok(studentBank);
            }
            catch (Exception ex)
            {
                string descripton = "Erro ao tentar alterar o registro do aluno " + student.Name + ". Erro: " + ex;
                await _logEntryController.CreateLogEntry(descripton, "Erro alterar registro");
                return StatusCode(500, $"Ocorreu um erro ao atualizar o estudante: {ex.Message}");
            }
        }

        [HttpGet("GetAllStudents")]
        public async Task<ActionResult<IEnumerable<StudentModel>>> GetAllStudents()
        {
            return await _context.Students.ToListAsync();
        }

        [HttpGet("GetStudentById/{id}")]
        public async Task<ActionResult<StudentModel>> GetStudentById(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound("Estudante não encontrado.");
            }
            return student;
        }

        [HttpGet("GetStudentByName/{name}")]
        public async Task<ActionResult<IEnumerable<StudentModel>>> GetStudentsByName(string name)
        {
            var students = await _context.Students.Where(s => s.Name.Contains(name)).ToListAsync();
            if (students == null || students.Count == 0)
            {
                return NotFound("Estudante não encontrado.");
            }
            return students;
        }


        //Incluir metodo Log aqui
        [HttpDelete("DeleteStudent/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound("Estudante não encontrado.");
            }

            try
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                string descripton = "Excluindo registro do aluno " + student.Name;
                await _logEntryController.CreateLogEntry(descripton, "Exclusão");
                return NoContent();
            }
            catch
            {
                string descripton = "Erro ao excluindo registro do aluno " + student.Name;
                await _logEntryController.CreateLogEntry(descripton, "Erro de exclusão");
                return BadRequest("Erro. Não foi possivel exluir o registro.");
            }

        }

        private async Task<IActionResult> ValidateStudent(StudentModel student, int? studentId = null)
        {
            var existingStudent = await _context.Students.FirstOrDefaultAsync(s => s.CPF == student.CPF && (!studentId.HasValue || s.StudentId != studentId.Value));
            if (existingStudent != null)
            {
                string descripton = "Erro ao tentar gravar o registro do aluno " + student.Name + ". CPF duplicado.";
                await _logEntryController.CreateLogEntry(descripton, "Erro novo registro");
                return BadRequest("Já existe um registro com esse CPF.");
            }

            List<int> genderIds = new List<int> { 1, 2, 3 };

            if (!genderIds.Contains(student.GenderId))
            {
                string descripton = "Erro ao tentar gravar o registro do aluno " + student.Name + ". Gênero inválido.";
                await _logEntryController.CreateLogEntry(descripton, "Erro novo registro");
                return BadRequest("Gênero inválido.");
            }

            //List<int> proficiencyIds = new List<int> { 1, 2, 3 };

            //if (!proficiencyIds.Contains(student.ProficiencyLevelId))
            //{
            //    string descripton = "Erro ao tentar gravar o registro do aluno " + student.Name + ". Nível de proficiência inválido.";
            //    await _logEntryController.CreateLogEntry(descripton, "Erro novo registro");
            //    return BadRequest("Nível de proficiência inválido.");
            //}

            return null;
        }

    }
}
