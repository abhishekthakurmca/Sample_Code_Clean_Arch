using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Added_Linked_Client_Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FromTime",
                table: "CompanyLinkedClients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToTime",
                table: "CompanyLinkedClients",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransportDays",
                table: "CompanyLinkedClients",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromTime",
                table: "CompanyLinkedClients");

            migrationBuilder.DropColumn(
                name: "ToTime",
                table: "CompanyLinkedClients");

            migrationBuilder.DropColumn(
                name: "TransportDays",
                table: "CompanyLinkedClients");
        }
    }
}
