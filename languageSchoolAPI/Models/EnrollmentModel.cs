namespace languageSchoolAPI.Models
{
    public class EnrollmentModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ClassroomId { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
