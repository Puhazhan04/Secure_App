using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecureApp.Migrations
{
    /// <inheritdoc />
    public partial class AddAbwesenheiten : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abwesenheiten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MitarbeiterId = table.Column<int>(type: "INTEGER", nullable: false),
                    Von = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Bis = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Grund = table.Column<string>(type: "TEXT", nullable: false),
                    Bemerkung = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abwesenheiten", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abwesenheiten_Mitarbeiter_MitarbeiterId",
                        column: x => x.MitarbeiterId,
                        principalTable: "Mitarbeiter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abwesenheiten_MitarbeiterId",
                table: "Abwesenheiten",
                column: "MitarbeiterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abwesenheiten");
        }
    }
}
