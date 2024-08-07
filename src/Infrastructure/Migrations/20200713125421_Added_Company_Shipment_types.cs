using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Added_Company_Shipment_types : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "CompanyShipmentTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company_Id = table.Column<long>(nullable: false),
                    ShipmentType_Id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyShipmentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyShipmentTypes_FreightCompany_ShipmentType_Id",
                        column: x => x.ShipmentType_Id,
                        principalTable: "FreightCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyShipmentTypes_ShipmentType_Id",
                table: "CompanyShipmentTypes",
                column: "ShipmentType_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyShipmentTypes");

            migrationBuilder.AddColumn<long>(
                name: "ShipmentType_Id",
                table: "LU_ShipmentTypes",
                type: "bigint",
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
    }
}
