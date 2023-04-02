using languageSchoolAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace languageSchoolAPI.Context
{
    public class LanguageSchoolContext : DbContext
    {
        public LanguageSchoolContext(DbContextOptions<LanguageSchoolContext> options) : base(options)
        {

        }

        public DbSet<StudentModel> Students { get; set; }
        public DbSet<EnrollmentModel> Enrollments { get; set; }
        public DbSet<TeacherModel> Teachers { get; set; }
        public DbSet<ClassroomModel> Classrooms { get; set; }

    }
}
