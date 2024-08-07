using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Update_Schedule_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DockTime",
                table: "ScheduleDates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dock",
                table: "DockScheduled",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DockTime",
                table: "ScheduleDates");

            migrationBuilder.DropColumn(
                name: "Dock",
                table: "DockScheduled");
        }
    }
}
