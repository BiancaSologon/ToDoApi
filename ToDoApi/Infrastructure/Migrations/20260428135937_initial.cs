using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApi.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ToDoApp");

            migrationBuilder.CreateTable(
                name: "Tag",
                schema: "ToDoApp",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToDoItem",
                schema: "ToDoApp",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    Secret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                schema: "ToDoApp",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDoItemId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_ToDoItem_ToDoItemId",
                        column: x => x.ToDoItemId,
                        principalSchema: "ToDoApp",
                        principalTable: "ToDoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToDoDetails",
                schema: "ToDoApp",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EstimatedMinutes = table.Column<int>(type: "int", nullable: false),
                    ToDoItemId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToDoDetails_ToDoItem_ToDoItemId",
                        column: x => x.ToDoItemId,
                        principalSchema: "ToDoApp",
                        principalTable: "ToDoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToDoItemTag",
                schema: "ToDoApp",
                columns: table => new
                {
                    TagId = table.Column<long>(type: "bigint", nullable: false),
                    ToDoItemId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoItemTag", x => new { x.TagId, x.ToDoItemId });
                    table.ForeignKey(
                        name: "FK_ToDoItemTag_Tag_TagId",
                        column: x => x.TagId,
                        principalSchema: "ToDoApp",
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToDoItemTag_ToDoItem_ToDoItemId",
                        column: x => x.ToDoItemId,
                        principalSchema: "ToDoApp",
                        principalTable: "ToDoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ToDoItemId",
                schema: "ToDoApp",
                table: "Comment",
                column: "ToDoItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ToDoDetails_ToDoItemId",
                schema: "ToDoApp",
                table: "ToDoDetails",
                column: "ToDoItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItemTag_ToDoItemId",
                schema: "ToDoApp",
                table: "ToDoItemTag",
                column: "ToDoItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment",
                schema: "ToDoApp");

            migrationBuilder.DropTable(
                name: "ToDoDetails",
                schema: "ToDoApp");

            migrationBuilder.DropTable(
                name: "ToDoItemTag",
                schema: "ToDoApp");

            migrationBuilder.DropTable(
                name: "Tag",
                schema: "ToDoApp");

            migrationBuilder.DropTable(
                name: "ToDoItem",
                schema: "ToDoApp");
        }
    }
}
