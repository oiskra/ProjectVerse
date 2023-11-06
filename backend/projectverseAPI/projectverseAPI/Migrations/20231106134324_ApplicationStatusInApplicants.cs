using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectverseAPI.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationStatusInApplicants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "CollaborationApplicants");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationStatus",
                table: "CollaborationApplicants",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationStatus",
                table: "CollaborationApplicants");

            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "CollaborationApplicants",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
