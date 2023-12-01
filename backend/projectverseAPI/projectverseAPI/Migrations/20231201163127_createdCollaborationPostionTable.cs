using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectverseAPI.Migrations
{
    /// <inheritdoc />
    public partial class createdCollaborationPostionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaborationApplicants_CollaborationPosition_AppliedPositionId",
                table: "CollaborationApplicants");

            migrationBuilder.DropForeignKey(
                name: "FK_CollaborationPosition_Collaborations_CollaborationId",
                table: "CollaborationPosition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollaborationPosition",
                table: "CollaborationPosition");

            migrationBuilder.RenameTable(
                name: "CollaborationPosition",
                newName: "CollaborationPositions");

            migrationBuilder.RenameIndex(
                name: "IX_CollaborationPosition_CollaborationId",
                table: "CollaborationPositions",
                newName: "IX_CollaborationPositions_CollaborationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollaborationPositions",
                table: "CollaborationPositions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaborationApplicants_CollaborationPositions_AppliedPositionId",
                table: "CollaborationApplicants",
                column: "AppliedPositionId",
                principalTable: "CollaborationPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CollaborationPositions_Collaborations_CollaborationId",
                table: "CollaborationPositions",
                column: "CollaborationId",
                principalTable: "Collaborations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaborationApplicants_CollaborationPositions_AppliedPositionId",
                table: "CollaborationApplicants");

            migrationBuilder.DropForeignKey(
                name: "FK_CollaborationPositions_Collaborations_CollaborationId",
                table: "CollaborationPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollaborationPositions",
                table: "CollaborationPositions");

            migrationBuilder.RenameTable(
                name: "CollaborationPositions",
                newName: "CollaborationPosition");

            migrationBuilder.RenameIndex(
                name: "IX_CollaborationPositions_CollaborationId",
                table: "CollaborationPosition",
                newName: "IX_CollaborationPosition_CollaborationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollaborationPosition",
                table: "CollaborationPosition",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaborationApplicants_CollaborationPosition_AppliedPositionId",
                table: "CollaborationApplicants",
                column: "AppliedPositionId",
                principalTable: "CollaborationPosition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CollaborationPosition_Collaborations_CollaborationId",
                table: "CollaborationPosition",
                column: "CollaborationId",
                principalTable: "Collaborations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
