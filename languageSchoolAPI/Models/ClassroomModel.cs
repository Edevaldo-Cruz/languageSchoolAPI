using NuGet.DependencyResolver;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace languageSchoolAPI.Models
{
    public class ClassroomModel
    {
        [Key]
        public int ClassroomId { get; set; }

        [Required(ErrorMessage = "O campo Course é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo Course deve ter no máximo 50 caracteres.")]
        public string Course { get; set; }

        [Required(ErrorMessage = "O campo TeacherId é obrigatório.")]
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public TeacherModel Teacher { get; set; }

        [Required(ErrorMessage = "O campo ProficiencyLevel é obrigatório.")]
        [Range(1, 10, ErrorMessage = "O campo ProficiencyLevel deve estar entre 1 e 10.")]
        public int ProficiencyLevel { get; set; }

        [Required(ErrorMessage = "O campo Time é obrigatório.")]
        public string Time { get; set; }

        [Required(ErrorMessage = "O campo Language é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo Language deve ter no máximo 50 caracteres.")]
        public string Language { get; set; }

        [Required(ErrorMessage = "O campo RoomNumber é obrigatório.")]
        [StringLength(10, ErrorMessage = "O campo RoomNumber deve ter no máximo 10 caracteres.")]
        public string RoomNumber { get; set; }
    }


}
