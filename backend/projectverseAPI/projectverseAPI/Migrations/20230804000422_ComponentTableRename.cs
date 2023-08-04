using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projectverseAPI.Migrations
{
    /// <inheritdoc />
    public partial class ComponentTableRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileComponents_Components_ComponentId",
                table: "ProfileComponents");

            migrationBuilder.DropTable(
                name: "Components");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileComponents_ComponentTypes_ComponentId",
                table: "ProfileComponents",
                column: "ComponentId",
                principalTable: "ComponentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileComponents_ComponentTypes_ComponentId",
                table: "ProfileComponents");

            migrationBuilder.DropTable(
                name: "ComponentTypes");

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileComponents_Components_ComponentId",
                table: "ProfileComponents",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
