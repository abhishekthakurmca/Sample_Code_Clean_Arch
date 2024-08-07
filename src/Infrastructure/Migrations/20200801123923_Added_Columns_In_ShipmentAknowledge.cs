using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Added_Columns_In_ShipmentAknowledge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmToken",
                table: "ShipmentAknowledge");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmFirstToken",
                table: "ShipmentAknowledge",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConfirmSecondToken",
                table: "ShipmentAknowledge",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConfirmThirdToken",
                table: "ShipmentAknowledge",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
