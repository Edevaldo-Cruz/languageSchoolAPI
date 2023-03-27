using System.ComponentModel.DataAnnotations;

namespace languageSchoolAPI.Models
{
    public class EnrollmentModel
    {
        [Key]
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int ClassroomId { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
