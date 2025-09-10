using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProblemStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TotalSubmisstion = table.Column<int>(type: "int", nullable: false),
                    AvgPoint = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemStats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Submission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProblemId = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Point = table.Column<int>(type: "int", nullable: false),
                    SubmisstionAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Algorithm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CleanCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProgress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TotalSubmisstion = table.Column<int>(type: "int", nullable: false),
                    EasySolved = table.Column<int>(type: "int", nullable: false),
                    MediumSolved = table.Column<int>(type: "int", nullable: false),
                    HardSolved = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProgress", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ProblemStats",
                columns: new[] { "Id", "AvgPoint", "TotalSubmisstion" },
                values: new object[,]
                {
                    { 1, 9, 1 },
                    { 2, 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Submission",
                columns: new[] { "Id", "Language", "Point", "ProblemId", "SubmisstionAt", "UserId", "Algorithm", "CleanCode" },
                values: new object[] { 1, "c sharp", 9, 1, new DateTime(2025, 9, 10, 9, 45, 48, 800, DateTimeKind.Local).AddTicks(4378), 2, "Algorithm is correct and efficient for the given task. No issues detected.", "Code is readable and follows basic C++ conventions. Could benefit from comments for clarity." });

            migrationBuilder.InsertData(
                table: "Submission",
                columns: new[] { "Id", "Algorithm", "CleanCode" },
                values: new object[] { 2, "Algorithm is correct and efficient for the given task. No issues detected.", "Code is readable and follows basic C++ conventions. Could benefit from comments for clarity." });

            migrationBuilder.InsertData(
                table: "UserProgress",
                columns: new[] { "Id", "EasySolved", "HardSolved", "MediumSolved", "Rank", "TotalSubmisstion" },
                values: new object[,]
                {
                    { 1, 1, 0, 0, 900, 1 },
                    { 2, 1, 0, 0, 1, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProblemStats");

            migrationBuilder.DropTable(
                name: "Submission");

            migrationBuilder.DropTable(
                name: "UserProgress");
        }
    }
}
