using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Aging_Dates_Shipments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDate",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "QuoteApprovalDate",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RejectShipmentDate",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TenderPendingApprovalDate",
                table: "RouteShipments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TenderPendingDate",
                table: "RouteShipments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "QuoteApprovalDate",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "RejectShipmentDate",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "TenderPendingApprovalDate",
                table: "RouteShipments");

            migrationBuilder.DropColumn(
                name: "TenderPendingDate",
                table: "RouteShipments");
        }
    }
}
