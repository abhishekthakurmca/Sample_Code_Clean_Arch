using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Update_Relationship_Route : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<long>(
                name: "Company_Id",
                table: "Route",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Route_Company_Id",
                table: "Route",
                column: "Company_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Route_FreightCompany_Company_Id",
                table: "Route",
                column: "Company_Id",
                principalTable: "FreightCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Route_FreightCompany_Company_Id",
                table: "Route");

            migrationBuilder.DropIndex(
                name: "IX_Route_Company_Id",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "Company_Id",
                table: "Route");

            migrationBuilder.AddColumn<long>(
                name: "Company_Id",
                table: "RouteShipments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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
    }
}
