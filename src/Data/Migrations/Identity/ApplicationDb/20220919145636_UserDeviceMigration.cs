using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityProvider.Data.Migrations.Identity.ApplicationDb
{
    public partial class UserDeviceMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDevices",
                columns: table => new
                {
                    User = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false),
                    Device = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    LastIp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastUsed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDevice", x => new { x.User, x.Device });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDevices");
        }
    }
}
