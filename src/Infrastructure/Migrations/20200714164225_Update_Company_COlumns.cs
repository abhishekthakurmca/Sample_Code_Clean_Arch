using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Update_Company_COlumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "FreightCompany",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "FreightCompany",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "FreightCompany",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "FreightCompany",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "FreightCompany",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentTerm",
                table: "FreightCompany",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "FreightCompany",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "FreightCompany",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "FreightCompany",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "FreightCompany");

            migrationBuilder.DropColumn(
                name: "Address1",
                table: "FreightCompany");

            migrationBuilder.DropColumn(
                name: "City",
                table: "FreightCompany");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "FreightCompany");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "FreightCompany");

            migrationBuilder.DropColumn(
                name: "PaymentTerm",
                table: "FreightCompany");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "FreightCompany");

            migrationBuilder.DropColumn(
                name: "State",
                table: "FreightCompany");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "FreightCompany");
        }
    }
}
