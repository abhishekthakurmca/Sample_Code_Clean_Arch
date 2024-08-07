using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Addcolumnsinroutshipmentandrouttableforpickuptime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DockDate",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DockTime",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InterCompanyShipmentId",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsInterCompany",
                table: "RouteShipments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PickUpDate",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PickUpTime",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PickUpTime",
                table: "Route",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DockDate",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "DockTime",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "InterCompanyShipmentId",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "IsInterCompany",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "PickUpDate",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "PickUpTime",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "PickUpTime",
                table: "Route");
        }
    }
}
