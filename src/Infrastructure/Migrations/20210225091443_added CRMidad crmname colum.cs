using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class addedCRMidadcrmnamecolum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CRM",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CRMId",
                table: "RouteShipments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CRM",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "CRMId",
                table: "RouteShipments");
        }
    }
}
