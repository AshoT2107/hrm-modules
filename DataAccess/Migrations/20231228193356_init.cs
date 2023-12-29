using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    birthdate = table.Column<string>(type: "text", nullable: false),
                    vacancy_type = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    institution = table.Column<string>(type: "text", nullable: true),
                    is_student = table.Column<bool>(type: "boolean", nullable: false),
                    study_form = table.Column<string>(type: "text", nullable: true),
                    english_level = table.Column<string>(type: "text", nullable: false),
                    app_proficiency = table.Column<string>(type: "text", nullable: true),
                    media = table.Column<string>(type: "text", nullable: false),
                    night_shift = table.Column<bool>(type: "boolean", nullable: false),
                    resume = table.Column<string>(type: "text", nullable: false),
                    source = table.Column<string>(type: "text", nullable: false),
                    register_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employees", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employees");
        }
    }
}
