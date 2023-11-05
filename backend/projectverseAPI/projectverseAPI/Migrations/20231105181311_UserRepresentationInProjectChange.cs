using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectverseAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserRepresentationInProjectChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_UserId1",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Projects",
                newName: "AuthorId1");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Projects",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_UserId1",
                table: "Projects",
                newName: "IX_Projects_AuthorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_AuthorId1",
                table: "Projects",
                column: "AuthorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_AuthorId1",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "AuthorId1",
                table: "Projects",
                newName: "UserId1");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Projects",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_AuthorId1",
                table: "Projects",
                newName: "IX_Projects_UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_UserId1",
                table: "Projects",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
