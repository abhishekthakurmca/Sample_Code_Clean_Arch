using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Updated_Tables_For_Contacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyShipmentTypes_FreightCompany_ShipmentType_Id",
                table: "CompanyShipmentTypes");

            migrationBuilder.DropTable(
                name: "CompanyContactTypes");

            migrationBuilder.AlterColumn<int>(
                name: "ShipmentType_Id",
                table: "CompanyShipmentTypes",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "CompanyShipmentFreightTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company_Id = table.Column<long>(nullable: false),
                    ShipmentFreight_Id = table.Column<int>(nullable: false)
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
                        name: "FK_CompanyShipmentFreightTypes_LU_ClientContactTypes_ShipmentFreight_Id",
                        column: x => x.ShipmentFreight_Id,
                        principalTable: "LU_ClientContactTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyShipmentTypes_Company_Id",
                table: "CompanyShipmentTypes",
                column: "Company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyShipmentFreightTypes_Company_Id",
                table: "CompanyShipmentFreightTypes",
                column: "Company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyShipmentFreightTypes_ShipmentFreight_Id",
                table: "CompanyShipmentFreightTypes",
                column: "ShipmentFreight_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyShipmentTypes_FreightCompany_Company_Id",
                table: "CompanyShipmentTypes",
                column: "Company_Id",
                principalTable: "FreightCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyShipmentTypes_LU_ShipmentTypes_ShipmentType_Id",
                table: "CompanyShipmentTypes",
                column: "ShipmentType_Id",
                principalTable: "LU_ShipmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyShipmentTypes_FreightCompany_Company_Id",
                table: "CompanyShipmentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyShipmentTypes_LU_ShipmentTypes_ShipmentType_Id",
                table: "CompanyShipmentTypes");

            migrationBuilder.DropTable(
                name: "CompanyShipmentFreightTypes");

            migrationBuilder.DropIndex(
                name: "IX_CompanyShipmentTypes_Company_Id",
                table: "CompanyShipmentTypes");

            migrationBuilder.AlterColumn<long>(
                name: "ShipmentType_Id",
                table: "CompanyShipmentTypes",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "CompanyContactTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company_Id = table.Column<long>(type: "bigint", nullable: false),
                    ContactType_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyContactTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyContactTypes_FreightCompany_Company_Id",
                        column: x => x.Company_Id,
                        principalTable: "FreightCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyContactTypes_LU_ClientContactTypes_ContactType_Id",
                        column: x => x.ContactType_Id,
                        principalTable: "LU_ClientContactTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContactTypes_Company_Id",
                table: "CompanyContactTypes",
                column: "Company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContactTypes_ContactType_Id",
                table: "CompanyContactTypes",
                column: "ContactType_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyShipmentTypes_FreightCompany_ShipmentType_Id",
                table: "CompanyShipmentTypes",
                column: "ShipmentType_Id",
                principalTable: "FreightCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
