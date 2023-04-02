using languageSchoolAPI.Context;
using languageSchoolAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace languageSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomsController : ControllerBase
    {
        private readonly LanguageSchoolContext _context;
        private readonly LogEntryController _logEntryController;

        public ClassroomsController(LanguageSchoolContext context, LogEntryController logEntryController)
        {
            _context = context;
            _logEntryController = logEntryController;
        }

        [HttpPost("CreateClassroom")]
        public async Task<ActionResult<ClassroomModel>> CreateClassroom(string? Course, int? TeacherId, int? ProficiencyLevel, string? Time, string? Language, string? RoomNumber)
        {                                

            if(Course == "" || Course == null)            
                return BadRequest("Campo Course é obrigatorio");
            if (TeacherId == 0 || TeacherId == null)
                return BadRequest("Campo TeacherId é obrigatorio");
            if (ProficiencyLevel == 0 || ProficiencyLevel == null)
                return BadRequest("Campo ProficiencyLevel é obrigatorio");
            if (Time == "" || Time == null)
                return BadRequest("Campo Time é obrigatorio");
            if (Language == "" || Language == null)
                return BadRequest("Campo Language é obrigatorio");
            if (RoomNumber == "" || RoomNumber == null)
                return BadRequest("Campo Language é obrigatorio");

            if (ProficiencyLevel != null || TeacherId != null)
            {
                int ProficiencyLevelValue = ProficiencyLevel.Value != null ? ProficiencyLevel.Value : 0;
                int TeacherIdValue = TeacherId.Value != null ? TeacherId.Value : 0;

                var validationResult = ValidateTeacherAndProficiencyLevel(ProficiencyLevelValue, TeacherIdValue);
                if (validationResult != null)
                    return validationResult;
            }

            try
            {
                ClassroomModel classroom = new ClassroomModel();
                classroom.Course = Course;
                classroom.TeacherId = TeacherId ?? 0;
                classroom.ProficiencyLevel = ProficiencyLevel ?? 0;
                classroom.Time = Time;
                classroom.Language = Language;
                classroom.RoomNumber = RoomNumber;

                _context.Classrooms.Add(classroom);
                await _context.SaveChangesAsync();

                string descripton = "Incluindo nova turma. Sala " + classroom.RoomNumber;
                await _logEntryController.CreateLogEntry(descripton, "Registrando uma nova turma.");
                return Ok(classroom);
            }
            catch (DbUpdateException ex)
            {
                string descripton = "Erro ao tentar gravar o registro da turma . Erro: " + ex;
                await _logEntryController.CreateLogEntry(descripton, "Erro ao registra nova turma.");
                return BadRequest(ex);
            }

        }

        [HttpPut("EditClassroom/{id}")]
        public async Task<ActionResult<ClassroomModel>> EditClassroom(int id, string? Course, int? TeacherId, int? ProficiencyLevel, string? Time, string? Language, string? RoomNumber)
        {
            var classroomtBank = await _context.Classrooms.FindAsync(id);
            if (classroomtBank == null)
                return BadRequest("Turma não encontrado.");

            if (ProficiencyLevel != null || TeacherId != null)
            {
                int ProficiencyLevelValue = ProficiencyLevel.HasValue ? ProficiencyLevel.Value : 0;
                int TeacherIdValue = TeacherId.HasValue ? TeacherId.Value : 0;

                var validationResult = ValidateTeacherAndProficiencyLevel(ProficiencyLevelValue, TeacherIdValue);
                if (validationResult != null)
                    return validationResult;
            }

            try
            {
                classroomtBank.Course = Course == null ? classroomtBank.Course : Course;
                classroomtBank.TeacherId = TeacherId == null ? classroomtBank.TeacherId : TeacherId.Value;
                classroomtBank.ProficiencyLevel = ProficiencyLevel == null ? classroomtBank.ProficiencyLevel : ProficiencyLevel.Value;
                classroomtBank.Time = Time == null || Time == null ? classroomtBank.Time : Time;
                classroomtBank.Language = Language == null ? classroomtBank.Language : Language;
                classroomtBank.RoomNumber = RoomNumber == null ? classroomtBank.RoomNumber : RoomNumber;

                _context.Classrooms.Update(classroomtBank);
                await _context.SaveChangesAsync();

                string descripton = "Alterando registro da turma " + classroomtBank.RoomNumber;
                await _logEntryController.CreateLogEntry(descripton, "Alteração de registro da turma");
                return Ok(classroomtBank);
            }
            catch (Exception ex)
            {
                string descripton = "Erro ao tentar alterar o registro da turma " + classroomtBank.RoomNumber + ". Erro: " + ex;
                await _logEntryController.CreateLogEntry(descripton, "Erro alterar registro turma");
                return StatusCode(422, "Ocorreu um erro ao atualizar o registro da turma." + ex.Message);
            }
        }

        [HttpGet("GetAllClassrooms")]
        public async Task<ActionResult<IEnumerable<ClassroomModel>>> GetAllClassrooms()
        {
            return await _context.Classrooms.ToListAsync();
        }

        [HttpGet("GetClassroomById/{id}")]
        public async Task<ActionResult<ClassroomModel>> GetClassroomById(int id)
        {
            var classroom = await _context.Classrooms.FindAsync(id);

            if (classroom == null)
            {
                return NotFound();
            }

            return classroom;
        }

        [HttpGet("GetClassroomByRoomNumber/{roomNumber}")]
        public async Task<ActionResult<IEnumerable<ClassroomModel>>> GetClassroomByRoomNumber(string roomNumber)
        {
            var classroom = await _context.Classrooms.Where(s => s.RoomNumber.Contains(roomNumber)).ToListAsync();
            if (classroom == null || classroom.Count == 0)
            {
                return NotFound("Turma não encontrado.");
            }
            return classroom;
        }

        [HttpGet("GetClassroomByLanguage/{language}")]
        public async Task<ActionResult<IEnumerable<ClassroomModel>>> GetClassroomByLanguage(string language)
        {
            var classroom = await _context.Classrooms.Where(s => s.Language.Contains(language)).ToListAsync();
            if (classroom == null || classroom.Count == 0)
            {
                return NotFound("Turma não encontrado.");
            }
            return classroom;
        }

        [HttpDelete("DeleteClassroom/{id}")]
        public async Task<IActionResult> DeleteClassroom(int id)
        {
            var classroom = await _context.Classrooms.FindAsync(id);
            if (classroom == null)
            {
                return NotFound("Turma não encontrada.");
            }

            try
            {
                _context.Classrooms.Remove(classroom);
                await _context.SaveChangesAsync();
                string descripton = "Excluindo registro da turma " + classroom.RoomNumber;
                await _logEntryController.CreateLogEntry(descripton, "Exclusão turma");
                return NoContent();
            }
            catch
            {
                string descripton = "Erro ao excluindo registro da " + classroom.RoomNumber;
                await _logEntryController.CreateLogEntry(descripton, "Erro de exclusão turma.");
                return BadRequest("Erro. Não foi possivel exluir o registro.");
            }
        }

        private ActionResult<ClassroomModel> ValidateTeacherAndProficiencyLevel(int proficiencyLevel, int teacherId)
        {
            if (proficiencyLevel < 1 || proficiencyLevel > 3)
            {
                return BadRequest("O campo ProficiencyLevel deve estar entre 1 e 3.");
            }

            var validTeacher = _context.Teachers.Any(x => x.TeacherId == teacherId);
            if (!validTeacher)
            {
                return BadRequest("Professor invalido");
            }

            return null;
        }


    }
}
