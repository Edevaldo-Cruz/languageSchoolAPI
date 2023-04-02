using NuGet.DependencyResolver;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace languageSchoolAPI.Models
{
    public class ClassroomModel
    {
        [Key]
        public int ClassroomId { get; set; }


        [StringLength(50, ErrorMessage = "O campo Course deve ter no máximo 50 caracteres.")]
        public string Course { get; set; }

        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }

        [RegularExpression("^[1-3]$", ErrorMessage = "O campo ProficiencyLevel deve estar entre 1 e 3.")]
        public int ProficiencyLevel { get; set; }

        public string Time { get; set; }

        [StringLength(50, ErrorMessage = "O campo Language deve ter no máximo 50 caracteres.")]
        public string Language { get; set; }

        [StringLength(10, ErrorMessage = "O campo RoomNumber deve ter no máximo 10 caracteres.")]
        public string RoomNumber { get; set; }

        public virtual TeacherModel Teacher { get; set; }
    }
}
