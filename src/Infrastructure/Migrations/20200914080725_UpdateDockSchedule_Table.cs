using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class UpdateDockSchedule_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceivingType",
                table: "DockScheduled",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Site",
                table: "DockScheduled",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceivingType",
                table: "DockScheduled");

            migrationBuilder.DropColumn(
                name: "Site",
                table: "DockScheduled");
        }
    }
}
