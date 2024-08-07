using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Add_Roue_Table_Relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "RouteShipments");

            migrationBuilder.AddColumn<long>(
                name: "Company_Id",
                table: "RouteShipments",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsAuto",
                table: "RouteShipments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "Route_Id",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RouteShipments_Company_Id",
                table: "RouteShipments",
                column: "Company_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RouteShipments_FreightCompany_Company_Id",
                table: "RouteShipments",
                column: "Company_Id",
                principalTable: "FreightCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteShipments_FreightCompany_Company_Id",
                table: "RouteShipments");

            migrationBuilder.DropIndex(
                name: "IX_RouteShipments_Company_Id",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "Company_Id",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "IsAuto",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "Route_Id",
                table: "RouteShipments");

            migrationBuilder.AddColumn<long>(
                name: "RouteId",
                table: "RouteShipments",
                type: "bigint",
                nullable: true);
        }
    }
}
