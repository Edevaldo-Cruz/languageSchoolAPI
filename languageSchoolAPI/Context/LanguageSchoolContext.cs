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
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<EnrollmentModel> Enrollments { get; set; }
        public DbSet<TeacherModel> Teachers { get; set; }
        public DbSet<ClassroomModel> Classrooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StudentModel>().HasIndex(s => s.CPF).IsUnique();

            modelBuilder.Entity<Gender>().HasData(
                new Gender { Id = 1, Name = "Masculino" },
                new Gender { Id = 2, Name = "Feminino" },
                new Gender { Id = 3, Name = "Não-binário" }
            );

            modelBuilder.Entity<ProficiencyLevel>().HasData(
                new ProficiencyLevel { Id = 1, Name = "Básico", Level = 1 },
                new ProficiencyLevel { Id = 2, Name = "Intermediário", Level = 2 },
                new ProficiencyLevel { Id = 3, Name = "Avançado", Level = 3 }
            );
        }



    }


}
