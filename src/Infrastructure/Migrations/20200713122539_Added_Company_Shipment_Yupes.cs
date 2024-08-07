using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Added_Company_Shipment_Yupes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ShipmentType_Id",
                table: "LU_ShipmentTypes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LU_ShipmentTypes_ShipmentType_Id",
                table: "LU_ShipmentTypes",
                column: "ShipmentType_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LU_ShipmentTypes_FreightCompany_ShipmentType_Id",
                table: "LU_ShipmentTypes",
                column: "ShipmentType_Id",
                principalTable: "FreightCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LU_ShipmentTypes_FreightCompany_ShipmentType_Id",
                table: "LU_ShipmentTypes");

            migrationBuilder.DropIndex(
                name: "IX_LU_ShipmentTypes_ShipmentType_Id",
                table: "LU_ShipmentTypes");

            migrationBuilder.DropColumn(
                name: "ShipmentType_Id",
                table: "LU_ShipmentTypes");
        }
    }
}
