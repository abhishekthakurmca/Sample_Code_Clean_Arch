using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Update_columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArrangedByCustomer",
                table: "RouteShipments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DroppedTrailer",
                table: "ManageClients",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FreightElevator",
                table: "ManageClients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArrangedByCustomer",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "DroppedTrailer",
                table: "ManageClients");

            migrationBuilder.DropColumn(
                name: "FreightElevator",
                table: "ManageClients");
        }
    }
}
