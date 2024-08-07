using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Tables_Remove_Fk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyShipmentFreightTypes_LU_ClientContactTypes_ShipmentFreight_Id",
                table: "CompanyShipmentFreightTypes");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyShipmentFreightTypes_LU_ShipmentFreightTypes_ShipmentFreight_Id",
                table: "CompanyShipmentFreightTypes",
                column: "ShipmentFreight_Id",
                principalTable: "LU_ShipmentFreightTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyShipmentFreightTypes_LU_ShipmentFreightTypes_ShipmentFreight_Id",
                table: "CompanyShipmentFreightTypes");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyShipmentFreightTypes_LU_ClientContactTypes_ShipmentFreight_Id",
                table: "CompanyShipmentFreightTypes",
                column: "ShipmentFreight_Id",
                principalTable: "LU_ClientContactTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
