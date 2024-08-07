using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Added_Token_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmFirstToken",
                table: "ShipmentAknowledge");

            migrationBuilder.DropColumn(
                name: "ConfirmSecondToken",
                table: "ShipmentAknowledge");

            migrationBuilder.DropColumn(
                name: "ConfirmThirdToken",
                table: "ShipmentAknowledge");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmToken",
                table: "ShipmentAknowledge",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmToken",
                table: "ShipmentAknowledge");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmFirstToken",
                table: "ShipmentAknowledge",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConfirmSecondToken",
                table: "ShipmentAknowledge",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConfirmThirdToken",
                table: "ShipmentAknowledge",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
