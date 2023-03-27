using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace languageSchoolAPI.Migrations
{
    /// <inheritdoc />
    public partial class teacherTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "TeacherId", "Name", "CPF", "Phone", "Address", "Email", "Birthdate", "Gender", "Nationality", "Observation" },
                values: new object[,]
                {
          { 1, "João da Silva Santos", "123.456.789-10", "(11) 1111-2222", "Rua Alegria, 123, apto 101", "joao.silva.santos@gmail.com", new DateTime(1985, 7, 15), "Masculino", "Brasileiro", " " },
          { 2, "Maria da Silva Souza", "234.567.890-21", "(22) 2222-3333", "Rua Esperança, 456, casa 02", "maria.silva.souza@hotmail.com", new DateTime(1979, 4, 20), "Feminino", "Brasileira", " " },
          { 3, "José da Costa Santos", "345.678.901-32", "(33) 3333-4444", "Rua Liberdade, 789, apto 402", "jose.costa.santos@yahoo.com", new DateTime(1992, 1, 5), "Masculino", "Português", " " },
          { 4, "Ana Paula de Almeida", "456.789.012-43", "(44) 4444-5555", "Rua Harmonia, 1010, casa 01", "ana.paula.almeida@gmail.com", new DateTime(1986, 11, 30), "Feminino", "Brasileira", " " },
          { 5, "Carlos Eduardo Pereira", "567.890.123-54", "(55) 5555-6666", "Avenida Brasil, 1111, apto 501", "carlos.pereira@yahoo.com", new DateTime(1983, 9, 15), "Masculino", "Brasileiro", " " },
          { 6, "Amanda Costa e Silva", "678.901.234-65", "(66) 6666-7777", "Rua dos Pinheiros, 2222, apto 301", "amanda.costa.silva@hotmail.com", new DateTime(1990, 6, 10), "Feminino", "Brasileira", " " },
          { 7, "Pedro Henrique da Cruz", "789.012.345-76", "(77) 7777-8888", "Rua da Paz, 3333, casa 02", "pedro.henrique.cruz@gmail.com", new DateTime(1987, 8, 25), "Masculino", "Brasileiro", " " },
          { 8, "Luciana da Costa Oliveira", "890.123.456-87", "(88) 8888-9999", "Rua da Harmonia, 4444, apto 602", "luciana.costa.oliveira@yahoo.com", new DateTime(1982, 3, 12), "Feminino", "Brasileira", " " },
           { 9, "Laura Rodriguez", "999.999.999-99", "(11) 91234-5678", "Av. Paulista, 987", "laura.rodriguez@teste.com", new DateTime(1992, 8, 17), "Feminino", "Espanhola", "Observação sobre a professora Laura Rodriguez." }
           });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Teachers");
        }
    }
}
