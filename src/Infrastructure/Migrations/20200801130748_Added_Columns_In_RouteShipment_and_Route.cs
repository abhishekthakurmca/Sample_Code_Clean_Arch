using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Added_Columns_In_RouteShipment_and_Route : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InternalNotes",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InternalReferenceID",
                table: "RouteShipments",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Quote",
                table: "RouteShipments",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DockDate",
                table: "Route",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "DockID",
                table: "Route",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "PickUpDate",
                table: "Route",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TruckSize",
                table: "Route",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InternalNotes",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "InternalReferenceID",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "Quote",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "DockDate",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "DockID",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "PickUpDate",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "TruckSize",
                table: "Route");
        }
    }
}
