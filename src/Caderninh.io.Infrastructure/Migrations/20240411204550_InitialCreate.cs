using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Caderninh.io.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NoteCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    NoteIds = table.Column<string>(type: "TEXT", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteCategories", x => x.Id);
                    table.UniqueConstraint("AK_NoteCategories_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NoteCategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Body = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_NoteCategories_NoteCategoryId",
                        column: x => x.NoteCategoryId,
                        principalTable: "NoteCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NoteCategories_Name_Id",
                table: "NoteCategories",
                columns: new[] { "Name", "Id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_Body_Id",
                table: "Notes",
                columns: new[] { "Body", "Id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_NoteCategoryId",
                table: "Notes",
                column: "NoteCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "NoteCategories");
        }
    }
}
