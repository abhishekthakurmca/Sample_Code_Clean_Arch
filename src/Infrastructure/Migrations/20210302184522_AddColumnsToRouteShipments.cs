using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class AddColumnsToRouteShipments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CRMEmail",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CRMName",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CRMPhone",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientReferenceNumber",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPhone",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "RouteShipments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CRMEmail",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "CRMName",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "CRMPhone",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "ClientReferenceNumber",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "ContactPhone",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "RouteShipments");
        }
    }
}
