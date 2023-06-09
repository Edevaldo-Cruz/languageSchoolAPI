﻿using Microsoft.EntityFrameworkCore.Migrations;

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
                columns: new[] { "TeacherId", "Name", "CPF", "Phone", "Address", "Email", "Birthdate", "GenderId", "Nationality", "Observation" },
                values: new object[,]
                {
                    { 1, "João da Silva Santos", "12345678910", "+55 (11) 91234-5678", "Rua Alegria, 123, apto 101", "joao.silva.santos@gmail.com", new DateTime(1985, 7, 15), 1, "Brasileiro", " " },
                    { 2, "Marie Dubois", "23456789021", "+33 6 12 34 56 78", "Rue de l'Espoir, 456, maison 02", "marie.dubois@hotmail.fr", new DateTime(1979, 4, 20), 2, "Americana", " " },
                    { 3, "John Smith", "34567890132", "+1 (415) 123-4567", "123 Main St, Apt 402", "john.smith@gmail.com", new DateTime(1992, 1, 5), 1, "Americano", " " },
                    { 4, "Ana Paula de Almeida", "45678901243", "+55 (21) 98765-4321", "Rua Harmonia, 1010, casa 01", "ana.paula.almeida@gmail.com", new DateTime(1986, 11, 30), 2, "Brasileira", " " },
                    { 5, "Carlos Eduardo Pereira", "56789012354", "+55 (11) 2345-6789", "Avenida Brasil, 1111, apto 501", "carlos.pereira@yahoo.com", new DateTime(1983, 9, 15), 1, "Brasileiro", " " },
                    { 6, "Amélie Martin", "67890123465", "+33 6 23 45 67 89", "Rue des Lilas, 2222, appartement 301", "amelie.martin@hotmail.fr", new DateTime(1990, 6, 10), 2, "Espanhola", " " },
                    { 7, "Pedro Henrique da Cruz", "78901234576", "+55 (11) 3456-7890", "Rua da Paz, 3333, casa 02", "pedro.henrique.cruz@gmail.com", new DateTime(1987, 8, 25), 1, "Brasileiro", " " },
                    { 8, "Luciana da Costa Oliveira", "89012345687", "+55 (11) 8765-4321", "Rua da Harmonia, 4444, apto 602", "luciana.costa.oliveira@yahoo.com", new  DateTime(1982, 3, 12), 2, "Brasileira", " " },
                    { 9, "Sofía Rodríguez", "99999999999", "+34 912 34 56 78", "Calle de Alcalá, 987", "sofia.rodriguez@teste.com", new DateTime(1992, 8, 17), 2, "Francesa", "Observação sobre a professora Sofía Rodríguez." }
                });
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Teachers");
        }
    }
}
