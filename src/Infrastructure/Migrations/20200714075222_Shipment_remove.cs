using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Shipment_remove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyContact_FreightCompany_FreightCompanyId",
                table: "CompanyContact");

            migrationBuilder.DropTable(
                name: "CompanyShipmentTypes");

            migrationBuilder.DropTable(
                name: "LU_ShipmentTypes");

            migrationBuilder.DropIndex(
                name: "IX_CompanyContact_FreightCompanyId",
                table: "CompanyContact");

            migrationBuilder.DropColumn(
                name: "FreightCompanyId",
                table: "CompanyContact");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContact_Company_Id",
                table: "CompanyContact",
                column: "Company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContact_ContactType_Id",
                table: "CompanyContact",
                column: "ContactType_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyContact_FreightCompany_Company_Id",
                table: "CompanyContact",
                column: "Company_Id",
                principalTable: "FreightCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyContact_LU_ClientContactTypes_ContactType_Id",
                table: "CompanyContact",
                column: "ContactType_Id",
                principalTable: "LU_ClientContactTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyContact_FreightCompany_Company_Id",
                table: "CompanyContact");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyContact_LU_ClientContactTypes_ContactType_Id",
                table: "CompanyContact");

            migrationBuilder.DropIndex(
                name: "IX_CompanyContact_Company_Id",
                table: "CompanyContact");

            migrationBuilder.DropIndex(
                name: "IX_CompanyContact_ContactType_Id",
                table: "CompanyContact");

            migrationBuilder.AddColumn<long>(
                name: "FreightCompanyId",
                table: "CompanyContact",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LU_ShipmentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Incoming = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LU_ShipmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyShipmentTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company_Id = table.Column<long>(type: "bigint", nullable: false),
                    ShipmentType_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyShipmentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyShipmentTypes_FreightCompany_Company_Id",
                        column: x => x.Company_Id,
                        principalTable: "FreightCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyShipmentTypes_LU_ShipmentTypes_ShipmentType_Id",
                        column: x => x.ShipmentType_Id,
                        principalTable: "LU_ShipmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContact_FreightCompanyId",
                table: "CompanyContact",
                column: "FreightCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyShipmentTypes_Company_Id",
                table: "CompanyShipmentTypes",
                column: "Company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyShipmentTypes_ShipmentType_Id",
                table: "CompanyShipmentTypes",
                column: "ShipmentType_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyContact_FreightCompany_FreightCompanyId",
                table: "CompanyContact",
                column: "FreightCompanyId",
                principalTable: "FreightCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
