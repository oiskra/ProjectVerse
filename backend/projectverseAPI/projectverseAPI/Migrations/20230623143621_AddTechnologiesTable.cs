using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectverseAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTechnologiesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technology_Collaborations_CollaborationId",
                table: "Technology");

            migrationBuilder.DropForeignKey(
                name: "FK_Technology_Projects_ProjectId",
                table: "Technology");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Technology",
                table: "Technology");

            migrationBuilder.RenameTable(
                name: "Technology",
                newName: "Technologies");

            migrationBuilder.RenameIndex(
                name: "IX_Technology_ProjectId",
                table: "Technologies",
                newName: "IX_Technologies_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Technology_CollaborationId",
                table: "Technologies",
                newName: "IX_Technologies_CollaborationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Technologies",
                table: "Technologies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Technologies_Collaborations_CollaborationId",
                table: "Technologies",
                column: "CollaborationId",
                principalTable: "Collaborations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Technologies_Projects_ProjectId",
                table: "Technologies",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technologies_Collaborations_CollaborationId",
                table: "Technologies");

            migrationBuilder.DropForeignKey(
                name: "FK_Technologies_Projects_ProjectId",
                table: "Technologies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Technologies",
                table: "Technologies");

            migrationBuilder.RenameTable(
                name: "Technologies",
                newName: "Technology");

            migrationBuilder.RenameIndex(
                name: "IX_Technologies_ProjectId",
                table: "Technology",
                newName: "IX_Technology_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Technologies_CollaborationId",
                table: "Technology",
                newName: "IX_Technology_CollaborationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Technology",
                table: "Technology",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Technology_Collaborations_CollaborationId",
                table: "Technology",
                column: "CollaborationId",
                principalTable: "Collaborations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Technology_Projects_ProjectId",
                table: "Technology",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
