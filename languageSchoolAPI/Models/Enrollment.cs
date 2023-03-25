namespace languageSchoolAPI.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ClassroomId { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
