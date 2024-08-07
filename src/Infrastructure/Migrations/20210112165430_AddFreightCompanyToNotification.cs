using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class AddFreightCompanyToNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FreightCompanyId",
                table: "InvoiceDetail",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetail_FreightCompanyId",
                table: "InvoiceDetail",
                column: "FreightCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetail_FreightCompany_FreightCompanyId",
                table: "InvoiceDetail",
                column: "FreightCompanyId",
                principalTable: "FreightCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetail_FreightCompany_FreightCompanyId",
                table: "InvoiceDetail");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceDetail_FreightCompanyId",
                table: "InvoiceDetail");

            migrationBuilder.DropColumn(
                name: "FreightCompanyId",
                table: "InvoiceDetail");
        }
    }
}
