using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace languageSchoolAPI.Models
{
    public class EnrollmentModel
    {
        [Key]
        public int EnrollmentId { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [ForeignKey("Classroom")]
        public int ClassroomId { get; set; }

        [Required(ErrorMessage = "A data de matrícula é obrigatória.")]
        public DateTime EnrollmentDate { get; set; }

        public virtual StudentModel Student { get; set; }

        public virtual ClassroomModel Classroom { get; set; }
    }

}
