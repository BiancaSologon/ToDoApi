using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApi.Migrations
{
    /// <inheritdoc />
    public partial class AddTagClassAndConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tag",
                schema: "ToDoApp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToDoItemTag",
                schema: "ToDoApp",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false),
                    ToDoItemId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_ToDoItemTag_ToDoItemId",
                schema: "ToDoApp",
                table: "ToDoItemTag",
                column: "ToDoItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDoItemTag",
                schema: "ToDoApp");

            migrationBuilder.DropTable(
                name: "Tag",
                schema: "ToDoApp");
        }
    }
}
