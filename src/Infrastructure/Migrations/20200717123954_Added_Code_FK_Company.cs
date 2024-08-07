using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Added_Code_FK_Company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FreightCompanyCodes_Company_Id",
                table: "FreightCompanyCodes",
                column: "Company_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FreightCompanyCodes_FreightCompany_Company_Id",
                table: "FreightCompanyCodes",
                column: "Company_Id",
                principalTable: "FreightCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FreightCompanyCodes_FreightCompany_Company_Id",
                table: "FreightCompanyCodes");

            migrationBuilder.DropIndex(
                name: "IX_FreightCompanyCodes_Company_Id",
                table: "FreightCompanyCodes");
        }
    }
}
