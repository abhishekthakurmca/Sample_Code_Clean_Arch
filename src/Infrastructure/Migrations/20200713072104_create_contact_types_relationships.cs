using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class create_contact_types_relationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FreightCompanyId",
                table: "CompanyContactTypes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LU_ClientContactTypesId",
                table: "CompanyContactTypes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContactTypes_FreightCompanyId",
                table: "CompanyContactTypes",
                column: "FreightCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContactTypes_LU_ClientContactTypesId",
                table: "CompanyContactTypes",
                column: "LU_ClientContactTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyContactTypes_FreightCompany_FreightCompanyId",
                table: "CompanyContactTypes",
                column: "FreightCompanyId",
                principalTable: "FreightCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyContactTypes_LU_ClientContactTypes_LU_ClientContactTypesId",
                table: "CompanyContactTypes",
                column: "LU_ClientContactTypesId",
                principalTable: "LU_ClientContactTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyContactTypes_FreightCompany_FreightCompanyId",
                table: "CompanyContactTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyContactTypes_LU_ClientContactTypes_LU_ClientContactTypesId",
                table: "CompanyContactTypes");

            migrationBuilder.DropIndex(
                name: "IX_CompanyContactTypes_FreightCompanyId",
                table: "CompanyContactTypes");

            migrationBuilder.DropIndex(
                name: "IX_CompanyContactTypes_LU_ClientContactTypesId",
                table: "CompanyContactTypes");

            migrationBuilder.DropColumn(
                name: "FreightCompanyId",
                table: "CompanyContactTypes");

            migrationBuilder.DropColumn(
                name: "LU_ClientContactTypesId",
                table: "CompanyContactTypes");
        }
    }
}
