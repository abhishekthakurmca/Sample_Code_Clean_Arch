using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Add_Weight_Route_Shipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "RouteShipments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "RouteShipments");
        }
    }
}
