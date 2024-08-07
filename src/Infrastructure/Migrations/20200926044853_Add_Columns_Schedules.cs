using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Add_Columns_Schedules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dock",
                table: "ClientSchedule",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DockId",
                table: "ClientSchedule",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReceivingType",
                table: "ClientSchedule",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReceivingTypeId",
                table: "ClientSchedule",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Site",
                table: "ClientSchedule",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteId",
                table: "ClientSchedule",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dock",
                table: "ClientSchedule");

            migrationBuilder.DropColumn(
                name: "DockId",
                table: "ClientSchedule");

            migrationBuilder.DropColumn(
                name: "ReceivingType",
                table: "ClientSchedule");

            migrationBuilder.DropColumn(
                name: "ReceivingTypeId",
                table: "ClientSchedule");

            migrationBuilder.DropColumn(
                name: "Site",
                table: "ClientSchedule");

            migrationBuilder.DropColumn(
                name: "SiteId",
                table: "ClientSchedule");
        }
    }
}
