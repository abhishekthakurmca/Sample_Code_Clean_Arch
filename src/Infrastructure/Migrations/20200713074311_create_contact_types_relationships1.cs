using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class create_contact_types_relationships1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<long>(
                name: "Company_Id",
                table: "CompanyContact",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContactTypes_Company_Id",
                table: "CompanyContactTypes",
                column: "Company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContactTypes_ContactType_Id",
                table: "CompanyContactTypes",
                column: "ContactType_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyContactTypes_FreightCompany_Company_Id",
                table: "CompanyContactTypes",
                column: "Company_Id",
                principalTable: "FreightCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyContactTypes_LU_ClientContactTypes_ContactType_Id",
                table: "CompanyContactTypes",
                column: "ContactType_Id",
                principalTable: "LU_ClientContactTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyContactTypes_FreightCompany_Company_Id",
                table: "CompanyContactTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyContactTypes_LU_ClientContactTypes_ContactType_Id",
                table: "CompanyContactTypes");

            migrationBuilder.DropIndex(
                name: "IX_CompanyContactTypes_Company_Id",
                table: "CompanyContactTypes");

            migrationBuilder.DropIndex(
                name: "IX_CompanyContactTypes_ContactType_Id",
                table: "CompanyContactTypes");

            migrationBuilder.AddColumn<long>(
                name: "FreightCompanyId",
                table: "CompanyContactTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LU_ClientContactTypesId",
                table: "CompanyContactTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Company_Id",
                table: "CompanyContact",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));

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
    }
}
