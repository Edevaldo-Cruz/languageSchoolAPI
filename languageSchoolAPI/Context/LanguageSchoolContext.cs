using languageSchoolAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace languageSchoolAPI.Context
{
    public class LanguageSchoolContext : DbContext
    {
        public LanguageSchoolContext(DbContextOptions<LanguageSchoolContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Classroom> Classrooms { get; set;}
        public DbSet<LogEntry> LogEntry { get; set; }

    }
}
