using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Add_Route_Shipment_Table_Relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RouteShipments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteId = table.Column<long>(nullable: false),
                    ShipmentId = table.Column<long>(nullable: false),
                    Tendered = table.Column<int>(nullable: false),
                    ShipmentStatus = table.Column<string>(nullable: true),
                    FreightType = table.Column<string>(nullable: true),
                    PlannedQty = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    ClientId = table.Column<long>(nullable: false),
                    Client = table.Column<string>(nullable: true),
                    CollectionPointId = table.Column<long>(nullable: false),
                    CollectionPoint = table.Column<string>(nullable: true),
                    CollectionPointAddress = table.Column<string>(nullable: true),
                    CollectionPointAddress2 = table.Column<string>(nullable: true),
                    CollectionPointCity = table.Column<string>(nullable: true),
                    CollectionPointState = table.Column<string>(nullable: true),
                    CollectionPointZip = table.Column<string>(nullable: true),
                    SiteId = table.Column<long>(nullable: false),
                    Site = table.Column<string>(nullable: true),
                    ShipmentProductId = table.Column<int>(nullable: false),
                    ShipmentProduct = table.Column<string>(nullable: true),
                    IsInbound = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteShipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentAknowledge",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    RouteShipment_Id = table.Column<long>(nullable: false),
                    ShipmentId = table.Column<long>(nullable: false),
                    RouteId = table.Column<long>(nullable: false),
                    ConfirmToken = table.Column<string>(nullable: true),
                    RejectionToken = table.Column<string>(nullable: true),
                    IsExpired = table.Column<bool>(nullable: false),
                    Triggered = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentAknowledge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentAknowledge_RouteShipments_RouteShipment_Id",
                        column: x => x.RouteShipment_Id,
                        principalTable: "RouteShipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentStatus",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    RouteShipment_Id = table.Column<long>(nullable: false),
                    ShipmentId = table.Column<long>(nullable: false),
                    RouteId = table.Column<long>(nullable: false),
                    OldStatus = table.Column<string>(nullable: true),
                    NewStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentStatus_RouteShipments_RouteShipment_Id",
                        column: x => x.RouteShipment_Id,
                        principalTable: "RouteShipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentAknowledge_RouteShipment_Id",
                table: "ShipmentAknowledge",
                column: "RouteShipment_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentStatus_RouteShipment_Id",
                table: "ShipmentStatus",
                column: "RouteShipment_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShipmentAknowledge");

            migrationBuilder.DropTable(
                name: "ShipmentStatus");

            migrationBuilder.DropTable(
                name: "RouteShipments");
        }
    }
}
