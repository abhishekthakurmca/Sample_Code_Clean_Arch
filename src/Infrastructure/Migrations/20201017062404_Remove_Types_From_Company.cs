using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Remove_Types_From_Company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyShipmentFreightTypes");

            migrationBuilder.DropTable(
                name: "LU_ShipmentFreightTypes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LU_ShipmentFreightTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LU_ShipmentFreightTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyShipmentFreightTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company_Id = table.Column<long>(type: "bigint", nullable: false),
                    ShipmentFreight_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyShipmentFreightTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyShipmentFreightTypes_FreightCompany_Company_Id",
                        column: x => x.Company_Id,
                        principalTable: "FreightCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyShipmentFreightTypes_LU_ShipmentFreightTypes_ShipmentFreight_Id",
                        column: x => x.ShipmentFreight_Id,
                        principalTable: "LU_ShipmentFreightTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyShipmentFreightTypes_Company_Id",
                table: "CompanyShipmentFreightTypes",
                column: "Company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyShipmentFreightTypes_ShipmentFreight_Id",
                table: "CompanyShipmentFreightTypes",
                column: "ShipmentFreight_Id");
        }
    }
}
