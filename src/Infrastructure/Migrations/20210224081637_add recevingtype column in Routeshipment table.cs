using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class addrecevingtypecolumninRouteshipmenttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceivingTypeName",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecevingTypeId",
                table: "RouteShipments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceivingTypeName",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "RecevingTypeId",
                table: "RouteShipments");
        }
    }
}
