using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Added_Email_In_Shipment_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Triggered",
                table: "ShipmentAknowledge");

            migrationBuilder.AddColumn<string>(
                name: "AccountType",
                table: "ShipmentAknowledge",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SentEmail",
                table: "ShipmentAknowledge",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "ShipmentAknowledge");

            migrationBuilder.DropColumn(
                name: "SentEmail",
                table: "ShipmentAknowledge");

            migrationBuilder.AddColumn<string>(
                name: "Triggered",
                table: "ShipmentAknowledge",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
