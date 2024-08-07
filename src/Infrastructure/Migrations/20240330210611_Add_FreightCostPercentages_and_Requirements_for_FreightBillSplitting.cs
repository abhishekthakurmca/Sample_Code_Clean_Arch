using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Add_FreightCostPercentages_and_Requirements_for_FreightBillSplitting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FreightCostResponsibilityPercentage",
                table: "RouteShipments",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "ManualFreightPercentageRequirement",
                table: "ManageClients",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreightCostResponsibilityPercentage",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "ManualFreightPercentageRequirement",
                table: "ManageClients");
        }
    }
}
