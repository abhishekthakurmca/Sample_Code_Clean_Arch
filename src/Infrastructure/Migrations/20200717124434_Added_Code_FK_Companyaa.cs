using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Added_Code_FK_Companyaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FreightCompanyCodes_FreightCategory_Id",
                table: "FreightCompanyCodes",
                column: "FreightCategory_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FreightCompanyCodes_FreightCategory_FreightCategory_Id",
                table: "FreightCompanyCodes",
                column: "FreightCategory_Id",
                principalTable: "FreightCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FreightCompanyCodes_FreightCategory_FreightCategory_Id",
                table: "FreightCompanyCodes");

            migrationBuilder.DropIndex(
                name: "IX_FreightCompanyCodes_FreightCategory_Id",
                table: "FreightCompanyCodes");
        }
    }
}
