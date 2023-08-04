using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectverseAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborations_Users_AuthorId",
                table: "Collaborations");

            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_Users_AuthorId",
                table: "PostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Users_UserId",
                table: "Profiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_UserId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Projects_UserId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_PostComments_AuthorId",
                table: "PostComments");

            migrationBuilder.DropIndex(
                name: "IX_Collaborations_AuthorId",
                table: "Collaborations");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Profiles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "PostComments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Collaborations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserId",
                table: "Projects",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_AuthorId",
                table: "PostComments",
                column: "AuthorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborations_AuthorId",
                table: "Collaborations",
                column: "AuthorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborations_AspNetUsers_AuthorId",
                table: "Collaborations",
                column: "AuthorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_AspNetUsers_AuthorId",
                table: "PostComments",
                column: "AuthorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_AspNetUsers_UserId",
                table: "Profiles",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_UserId",
                table: "Projects",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborations_AspNetUsers_AuthorId1",
                table: "Collaborations");

            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_AspNetUsers_AuthorId1",
                table: "PostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_AspNetUsers_UserId1",
                table: "Profiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_UserId1",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_UserId1",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_UserId1",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_PostComments_AuthorId1",
                table: "PostComments");

            migrationBuilder.DropIndex(
                name: "IX_Collaborations_AuthorId1",
                table: "Collaborations");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "PostComments");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "Collaborations");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserId",
                table: "Projects",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_AuthorId",
                table: "PostComments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborations_AuthorId",
                table: "Collaborations",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborations_Users_AuthorId",
                table: "Collaborations",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_Users_AuthorId",
                table: "PostComments",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Users_UserId",
                table: "Profiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_UserId",
                table: "Projects",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
