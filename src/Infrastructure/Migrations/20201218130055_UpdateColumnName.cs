using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class UpdateColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImporAssettPrice",
                table: "InvoiceDetail");

            migrationBuilder.AddColumn<float>(
                name: "ImportAssetPrice",
                table: "InvoiceDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImportAssetPrice",
                table: "InvoiceDetail");

            migrationBuilder.AddColumn<float>(
                name: "ImporAssettPrice",
                table: "InvoiceDetail",
                type: "real",
                nullable: true);
        }
    }
}
