using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectverseAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserProfileTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificate_Profiles_ProfileId",
                table: "Certificate");

            migrationBuilder.DropForeignKey(
                name: "FK_Education_Profiles_ProfileId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Interest_Profiles_ProfileId",
                table: "Interest");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTechnologyStack_Profiles_ProfileId",
                table: "UserTechnologyStack");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "UserTechnologyStack",
                newName: "UserProfileDataId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTechnologyStack_ProfileId",
                table: "UserTechnologyStack",
                newName: "IX_UserTechnologyStack_UserProfileDataId");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "Interest",
                newName: "UserProfileDataId");

            migrationBuilder.RenameIndex(
                name: "IX_Interest_ProfileId",
                table: "Interest",
                newName: "IX_Interest_UserProfileDataId");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "Education",
                newName: "UserProfileDataId");

            migrationBuilder.RenameIndex(
                name: "IX_Education_ProfileId",
                table: "Education",
                newName: "IX_Education_UserProfileDataId");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "Certificate",
                newName: "UserProfileDataId");

            migrationBuilder.RenameIndex(
                name: "IX_Certificate_ProfileId",
                table: "Certificate",
                newName: "IX_Certificate_UserProfileDataId");

            migrationBuilder.CreateTable(
                name: "UserProfileData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AboutMe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Achievements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryTechnology = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfileData_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SocialMedia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserProfileDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialMedia_UserProfileData_UserProfileDataId",
                        column: x => x.UserProfileDataId,
                        principalTable: "UserProfileData",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedia_UserProfileDataId",
                table: "SocialMedia",
                column: "UserProfileDataId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileData_UserId1",
                table: "UserProfileData",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificate_UserProfileData_UserProfileDataId",
                table: "Certificate",
                column: "UserProfileDataId",
                principalTable: "UserProfileData",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Education_UserProfileData_UserProfileDataId",
                table: "Education",
                column: "UserProfileDataId",
                principalTable: "UserProfileData",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Interest_UserProfileData_UserProfileDataId",
                table: "Interest",
                column: "UserProfileDataId",
                principalTable: "UserProfileData",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTechnologyStack_UserProfileData_UserProfileDataId",
                table: "UserTechnologyStack",
                column: "UserProfileDataId",
                principalTable: "UserProfileData",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificate_UserProfileData_UserProfileDataId",
                table: "Certificate");

            migrationBuilder.DropForeignKey(
                name: "FK_Education_UserProfileData_UserProfileDataId",
                table: "Education");

            migrationBuilder.DropForeignKey(
                name: "FK_Interest_UserProfileData_UserProfileDataId",
                table: "Interest");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTechnologyStack_UserProfileData_UserProfileDataId",
                table: "UserTechnologyStack");

            migrationBuilder.DropTable(
                name: "SocialMedia");

            migrationBuilder.DropTable(
                name: "UserProfileData");

            migrationBuilder.RenameColumn(
                name: "UserProfileDataId",
                table: "UserTechnologyStack",
                newName: "ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTechnologyStack_UserProfileDataId",
                table: "UserTechnologyStack",
                newName: "IX_UserTechnologyStack_ProfileId");

            migrationBuilder.RenameColumn(
                name: "UserProfileDataId",
                table: "Interest",
                newName: "ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Interest_UserProfileDataId",
                table: "Interest",
                newName: "IX_Interest_ProfileId");

            migrationBuilder.RenameColumn(
                name: "UserProfileDataId",
                table: "Education",
                newName: "ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Education_UserProfileDataId",
                table: "Education",
                newName: "IX_Education_ProfileId");

            migrationBuilder.RenameColumn(
                name: "UserProfileDataId",
                table: "Certificate",
                newName: "ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Certificate_UserProfileDataId",
                table: "Certificate",
                newName: "IX_Certificate_ProfileId");

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AboutMe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Achievements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryTechnology = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondaryTechnology = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId1",
                table: "Profiles",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificate_Profiles_ProfileId",
                table: "Certificate",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Education_Profiles_ProfileId",
                table: "Education",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Interest_Profiles_ProfileId",
                table: "Interest",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTechnologyStack_Profiles_ProfileId",
                table: "UserTechnologyStack",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id");
        }
    }
}
