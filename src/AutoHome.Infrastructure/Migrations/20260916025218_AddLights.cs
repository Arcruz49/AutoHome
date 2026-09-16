using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoHome.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLights : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DeviceId = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    IpAddress = table.Column<string>(type: "TEXT", maxLength: 45, nullable: false),
                    SupportsColour = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lights", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lights_DeviceId",
                table: "Lights",
                column: "DeviceId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lights");
        }
    }
}
