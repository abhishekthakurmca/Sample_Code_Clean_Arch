using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Update_TenderEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsContractNotification",
                table: "CompanyContact");

            migrationBuilder.DropColumn(
                name: "IsPrimaryContact",
                table: "CompanyContact");

            migrationBuilder.AddColumn<string>(
                name: "TenderEmail",
                table: "FreightCompany",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenderEmail",
                table: "FreightCompany");

            migrationBuilder.AddColumn<bool>(
                name: "IsContractNotification",
                table: "CompanyContact",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimaryContact",
                table: "CompanyContact",
                type: "bit",
                nullable: true);
        }
    }
}
