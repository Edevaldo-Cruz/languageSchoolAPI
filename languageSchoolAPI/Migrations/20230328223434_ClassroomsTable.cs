using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace languageSchoolAPI.Migrations
{
    /// <inheritdoc />
    public partial class ClassroomsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                 table: "Classrooms",
                 columns: new[] { "ClassroomId", "Course", "Teacher", "ProficiencyLevel", "Time", "Language", "RoomNumber" },
                 values: new object[,]
                 {
                    { 1, "Introdução ao Inglês", 1, 1, "Segunda-feira, 19h-21h", "Inglês", "101A" },
                    { 2, "Inglês Intermediário", 2, 2, "Terça-feira, 18h-20h", "Inglês", "102A" },
                    { 3, "Inglês Avançado", 3, 3, "Quarta-feira, 20h-22h", "Inglês", "103A" },
                    { 4, "Espanhol para iniciantes", 4, 1, "Quinta-feira, 18h-20h", "Espanhol", "201B" },
                    { 5, "Espanhol Intermediário", 5, 2, "Sexta-feira, 19h-21h", "Espanhol", "202B" },
                    { 6, "Espanhol Avançado", 6, 3, "Sábado, 10h-12h", "Espanhol", "203B" },
                    { 7, "Francês para iniciantes", 7, 1, "Domingo, 15h-17h", "Francês", "301C" },
                    { 8, "Francês Intermediário", 8, 2, "Segunda-feira, 14h-16h", "Francês", "302C" },
                    { 9, "Francês Avançado", 9, 3, "Quinta-feira, 10h-12h", "Francês", "303C" },
                 });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Classrooms");
        }
    }
}
