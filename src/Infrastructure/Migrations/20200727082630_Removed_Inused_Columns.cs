using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Removed_Inused_Columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "ShipmentAknowledge");

            migrationBuilder.DropColumn(
                name: "ShipmentId",
                table: "ShipmentAknowledge");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RouteId",
                table: "ShipmentAknowledge",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ShipmentId",
                table: "ShipmentAknowledge",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
