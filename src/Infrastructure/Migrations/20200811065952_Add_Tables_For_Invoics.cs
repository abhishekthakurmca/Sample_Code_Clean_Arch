using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Add_Tables_For_Invoics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceDocuments");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.CreateTable(
                name: "FreightInvoices",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Route_Id = table.Column<long>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    FreightCharges = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Quote = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18, 3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreightInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FreightInvoices_Route_Route_Id",
                        column: x => x.Route_Id,
                        principalTable: "Route",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FreightInvoiceItems",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    FreightInvoice_Id = table.Column<long>(nullable: false),
                    FreightCode_Id = table.Column<long>(nullable: false),
                    FreightCode = table.Column<string>(nullable: true),
                    FreightDescription = table.Column<string>(nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18, 3)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    InvoiceNumber = table.Column<string>(nullable: true),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    TransportingMiles = table.Column<long>(nullable: false),
                    InvoiceItem_Id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreightInvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FreightInvoiceItems_FreightCompanyCodes_FreightCode_Id",
                        column: x => x.FreightCode_Id,
                        principalTable: "FreightCompanyCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FreightInvoiceItems_FreightInvoices_InvoiceItem_Id",
                        column: x => x.InvoiceItem_Id,
                        principalTable: "FreightInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FreightInvoiceItems_FreightCode_Id",
                table: "FreightInvoiceItems",
                column: "FreightCode_Id");

            migrationBuilder.CreateIndex(
                name: "IX_FreightInvoiceItems_InvoiceItem_Id",
                table: "FreightInvoiceItems",
                column: "InvoiceItem_Id");

            migrationBuilder.CreateIndex(
                name: "IX_FreightInvoices_Route_Id",
                table: "FreightInvoices",
                column: "Route_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FreightInvoiceItems");

            migrationBuilder.DropTable(
                name: "FreightInvoices");

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreightCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreightCode_Id = table.Column<long>(type: "bigint", nullable: false),
                    FreightDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrandTotal = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Percentage = table.Column<decimal>(type: "decimal(18, 3)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18, 3)", nullable: false),
                    Quote = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Route_Id = table.Column<long>(type: "bigint", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    TransportingMiles = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_FreightCompanyCodes_FreightCode_Id",
                        column: x => x.FreightCode_Id,
                        principalTable: "FreightCompanyCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceItem_Id = table.Column<long>(type: "bigint", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
    }
}
