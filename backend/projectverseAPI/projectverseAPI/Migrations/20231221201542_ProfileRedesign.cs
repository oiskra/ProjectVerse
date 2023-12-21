using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectverseAPI.Migrations
{
    /// <inheritdoc />
    public partial class ProfileRedesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileComponents_ComponentThemes_ThemeId",
                table: "ProfileComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileComponents_ComponentTypes_ComponentId",
                table: "ProfileComponents");

            migrationBuilder.DropTable(
                name: "ComponentThemes");

            migrationBuilder.DropTable(
                name: "ComponentTypes");

            migrationBuilder.DropIndex(
                name: "IX_ProfileComponents_ComponentId",
                table: "ProfileComponents");

            migrationBuilder.DropColumn(
                name: "ComponentId",
                table: "ProfileComponents");

            migrationBuilder.RenameColumn(
                name: "ThemeId",
                table: "ProfileComponents",
                newName: "ProfileDesignerId");

            migrationBuilder.RenameColumn(
                name: "PositionY",
                table: "ProfileComponents",
                newName: "RowStart");

            migrationBuilder.RenameColumn(
                name: "PositionX",
                table: "ProfileComponents",
                newName: "RowEnd");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileComponents_ThemeId",
                table: "ProfileComponents",
                newName: "IX_ProfileComponents_ProfileDesignerId");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "ProfileComponents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ColumnEnd",
                table: "ProfileComponents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ColumnStart",
                table: "ProfileComponents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "ProfileComponents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "ProfileComponents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ProfileDesigners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileDesigners", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileComponents_ProfileDesigners_ProfileDesignerId",
                table: "ProfileComponents",
                column: "ProfileDesignerId",
                principalTable: "ProfileDesigners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileComponents_ProfileDesigners_ProfileDesignerId",
                table: "ProfileComponents");

            migrationBuilder.DropTable(
                name: "ProfileDesigners");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "ProfileComponents");

            migrationBuilder.DropColumn(
                name: "ColumnEnd",
                table: "ProfileComponents");

            migrationBuilder.DropColumn(
                name: "ColumnStart",
                table: "ProfileComponents");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "ProfileComponents");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ProfileComponents");

            migrationBuilder.RenameColumn(
                name: "RowStart",
                table: "ProfileComponents",
                newName: "PositionY");

            migrationBuilder.RenameColumn(
                name: "RowEnd",
                table: "ProfileComponents",
                newName: "PositionX");

            migrationBuilder.RenameColumn(
                name: "ProfileDesignerId",
                table: "ProfileComponents",
                newName: "ThemeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfileComponents_ProfileDesignerId",
                table: "ProfileComponents",
                newName: "IX_ProfileComponents_ThemeId");

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentId",
                table: "ProfileComponents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ComponentThemes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentThemes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComponentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileComponents_ComponentId",
                table: "ProfileComponents",
                column: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileComponents_ComponentThemes_ThemeId",
                table: "ProfileComponents",
                column: "ThemeId",
                principalTable: "ComponentThemes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileComponents_ComponentTypes_ComponentId",
                table: "ProfileComponents",
                column: "ComponentId",
                principalTable: "ComponentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
