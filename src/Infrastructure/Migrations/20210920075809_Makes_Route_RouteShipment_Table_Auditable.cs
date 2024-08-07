using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Makes_Route_RouteShipment_Table_Auditable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "RouteShipments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Route",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Route",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Route",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Route",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Route");
        }
    }
}
