using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectverseAPI.Migrations
{
    /// <inheritdoc />
    public partial class ApplicantTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollaborationApplicants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicantUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicantUserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AppliedPositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppliedCollaborationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppliedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaborationApplicants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollaborationApplicants_AspNetUsers_ApplicantUserId1",
                        column: x => x.ApplicantUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CollaborationApplicants_CollaborationPosition_AppliedPositionId",
                        column: x => x.AppliedPositionId,
                        principalTable: "CollaborationPosition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollaborationApplicants_Collaborations_AppliedCollaborationId",
                        column: x => x.AppliedCollaborationId,
                        principalTable: "Collaborations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationApplicants_ApplicantUserId1",
                table: "CollaborationApplicants",
                column: "ApplicantUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationApplicants_AppliedCollaborationId",
                table: "CollaborationApplicants",
                column: "AppliedCollaborationId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationApplicants_AppliedPositionId",
                table: "CollaborationApplicants",
                column: "AppliedPositionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaborationApplicants");
        }
    }
}
