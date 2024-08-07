using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class added_columns_in_RouteShipments_Table_Cantransfer_DockId_DockName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanTransfer",
                table: "RouteShipments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DockID",
                table: "RouteShipments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DockName",
                table: "RouteShipments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanTransfer",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "DockID",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "DockName",
                table: "RouteShipments");
        }
    }
}
