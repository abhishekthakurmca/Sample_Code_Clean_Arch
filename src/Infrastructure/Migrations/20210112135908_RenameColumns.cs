using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class RenameColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailedAssetPrice",
                table: "InvoiceDetail");

            migrationBuilder.DropColumn(
                name: "ImportAssetPrice",
                table: "InvoiceDetail");

            migrationBuilder.DropColumn(
                name: "OrginalFileURL",
                table: "InvoiceDetail");

            migrationBuilder.AddColumn<float>(
                name: "FailedFreightPrice",
                table: "InvoiceDetail",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ImportFreightPrice",
                table: "InvoiceDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OriginalFileURL",
                table: "InvoiceDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailedFreightPrice",
                table: "InvoiceDetail");

            migrationBuilder.DropColumn(
                name: "ImportFreightPrice",
                table: "InvoiceDetail");

            migrationBuilder.DropColumn(
                name: "OriginalFileURL",
                table: "InvoiceDetail");

            migrationBuilder.AddColumn<float>(
                name: "FailedAssetPrice",
                table: "InvoiceDetail",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ImportAssetPrice",
                table: "InvoiceDetail",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrginalFileURL",
                table: "InvoiceDetail",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
