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
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProblemId = table.Column<int>(type: "int", nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Like = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ParentCommentId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Comment_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "Id", "CommentText", "CreatedAt", "Like", "ParentCommentId", "ProblemId", "UserId" },
                values: new object[,]
                {
                    { 1, "This is a sample comment.", new DateTime(2025, 9, 8, 3, 0, 16, 336, DateTimeKind.Utc).AddTicks(47), 10, null, 1, 1 },
                    { 3, "Another top-level comment.", new DateTime(2025, 9, 8, 3, 0, 16, 336, DateTimeKind.Utc).AddTicks(52), 2, null, 1, 2 },
                    { 2, "This is a reply to the sample comment.", new DateTime(2025, 9, 8, 3, 0, 16, 336, DateTimeKind.Utc).AddTicks(50), 5, 1, 1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ParentCommentId",
                table: "Comment",
                column: "ParentCommentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");
        }
    }
}
