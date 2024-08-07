using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Added_docktime_Column_in_Route_And_Dockscheduled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DockTime",
                table: "Route",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DockTime",
                table: "DockScheduled",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DockTime",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "DockTime",
                table: "DockScheduled");
        }
    }
}
