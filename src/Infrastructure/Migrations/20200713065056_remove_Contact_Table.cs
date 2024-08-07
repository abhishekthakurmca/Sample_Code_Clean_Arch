using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class remove_Contact_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyContact");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyContact",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedByID = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Direct = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ext = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreightCompanyId = table.Column<int>(type: "int", nullable: false),
                    FreightCompanyId1 = table.Column<long>(type: "bigint", nullable: true),
                    IsChannelSalesRep = table.Column<bool>(type: "bit", nullable: true),
                    IsContractNotification = table.Column<bool>(type: "bit", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    IsPrimaryContact = table.Column<bool>(type: "bit", nullable: true),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LU_ClientContactTypesId = table.Column<int>(type: "int", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyContact_FreightCompany_FreightCompanyId1",
                        column: x => x.FreightCompanyId1,
                        principalTable: "FreightCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyContact_LU_ClientContactTypes_LU_ClientContactTypesId",
                        column: x => x.LU_ClientContactTypesId,
                        principalTable: "LU_ClientContactTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContact_FreightCompanyId1",
                table: "CompanyContact",
                column: "FreightCompanyId1");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContact_LU_ClientContactTypesId",
                table: "CompanyContact",
                column: "LU_ClientContactTypesId");
        }
    }
}
