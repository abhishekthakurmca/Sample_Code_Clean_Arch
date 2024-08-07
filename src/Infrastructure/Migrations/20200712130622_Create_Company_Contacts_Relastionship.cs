using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Create_Company_Contacts_Relastionship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FreightCompanyId",
                table: "CompanyContact",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContact_FreightCompanyId",
                table: "CompanyContact",
                column: "FreightCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyContact_FreightCompany_FreightCompanyId",
                table: "CompanyContact",
                column: "FreightCompanyId",
                principalTable: "FreightCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyContact_FreightCompany_FreightCompanyId",
                table: "CompanyContact");

            migrationBuilder.DropIndex(
                name: "IX_CompanyContact_FreightCompanyId",
                table: "CompanyContact");

            migrationBuilder.DropColumn(
                name: "FreightCompanyId",
                table: "CompanyContact");
        }
    }
}
