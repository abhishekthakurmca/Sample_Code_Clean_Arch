using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Add_Columns_RejectReason_IsNeedsHelp_TransferdDate_Notes_NeedHelpNotes_HoldOverNotes_RouteUpdatedDate_Table_RouteShipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HoldOverNotes",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsNeedsHelp",
                table: "RouteShipments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NeedsHelpNote",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectReason",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RouteDate",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransferredDate",
                table: "RouteShipments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoldOverNotes",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "IsNeedsHelp",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "NeedsHelpNote",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "RejectReason",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "RouteDate",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "TransferredDate",
                table: "RouteShipments");
        }
    }
}
