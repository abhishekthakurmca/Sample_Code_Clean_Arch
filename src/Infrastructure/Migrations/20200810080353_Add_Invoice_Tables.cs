using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Add_Invoice_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    FreightCode_Id = table.Column<long>(nullable: false),
                    Route_Id = table.Column<long>(nullable: false),
                    FreightCode = table.Column<string>(nullable: true),
                    FreightDescription = table.Column<string>(nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18, 3)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    InvoiceNumber = table.Column<string>(nullable: true),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    GrandTotal = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Quote = table.Column<decimal>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    TransportingMiles = table.Column<long>(nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18, 3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_FreightCompanyCodes_FreightCode_Id",
                        column: x => x.FreightCode_Id,
                        principalTable: "FreightCompanyCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Route_Route_Id",
                        column: x => x.Route_Id,
                        principalTable: "Route",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDocuments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    InvoiceItem_Id = table.Column<long>(nullable: false),
                    DocumentPath = table.Column<string>(nullable: true),
                    DocumentName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceDocuments_InvoiceItems_InvoiceItem_Id",
                        column: x => x.InvoiceItem_Id,
                        principalTable: "InvoiceItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDocuments_InvoiceItem_Id",
                table: "InvoiceDocuments",
                column: "InvoiceItem_Id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_FreightCode_Id",
                table: "InvoiceItems",
                column: "FreightCode_Id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_Route_Id",
                table: "InvoiceItems",
                column: "Route_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceDocuments");

            migrationBuilder.DropTable(
                name: "InvoiceItems");
        }
    }
}
