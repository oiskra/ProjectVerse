using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectverseAPI.Migrations
{
    /// <inheritdoc />
    public partial class CollabUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaborationApplicants_Collaborations_AppliedCollaborationId",
                table: "CollaborationApplicants");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Collaborations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PeopleInvolved",
                table: "Collaborations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "CollaborationApplicants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_CollaborationApplicants_Collaborations_AppliedCollaborationId",
                table: "CollaborationApplicants",
                column: "AppliedCollaborationId",
                principalTable: "Collaborations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaborationApplicants_Collaborations_AppliedCollaborationId",
                table: "CollaborationApplicants");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Collaborations");

            migrationBuilder.DropColumn(
                name: "PeopleInvolved",
                table: "Collaborations");

            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "CollaborationApplicants");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaborationApplicants_Collaborations_AppliedCollaborationId",
                table: "CollaborationApplicants",
                column: "AppliedCollaborationId",
                principalTable: "Collaborations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
