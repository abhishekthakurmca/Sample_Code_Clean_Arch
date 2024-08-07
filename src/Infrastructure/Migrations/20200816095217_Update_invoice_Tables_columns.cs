using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Update_invoice_Tables_columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FreightInvoiceItems_FreightInvoices_InvoiceItem_Id",
                table: "FreightInvoiceItems");

            migrationBuilder.DropIndex(
                name: "IX_FreightInvoiceItems_InvoiceItem_Id",
                table: "FreightInvoiceItems");

            migrationBuilder.DropColumn(
                name: "BOINumber",
                table: "FreightInvoices");

            migrationBuilder.DropColumn(
                name: "FreightCharges",
                table: "FreightInvoices");

            migrationBuilder.DropColumn(
                name: "InvoiceDate",
                table: "FreightInvoices");

            migrationBuilder.DropColumn(
                name: "InvoiceName",
                table: "FreightInvoices");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "FreightInvoices");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "FreightInvoices");

            migrationBuilder.DropColumn(
                name: "TransportingMiles",
                table: "FreightInvoices");

            migrationBuilder.DropColumn(
                name: "FreightInvoice_Id",
                table: "FreightInvoiceItems");

            migrationBuilder.DropColumn(
                name: "InvoiceItem_Id",
                table: "FreightInvoiceItems");

            migrationBuilder.AlterColumn<decimal>(
                name: "GrandTotal",
                table: "FreightInvoices",
                type: "decimal(18, 6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<long>(
                name: "FreightInvoiceHeader_Id",
                table: "FreightInvoiceItems",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "FreightInvoiceHeaders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    FreightInvoice_Id = table.Column<long>(nullable: false),
                    FreightCharges = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    InvoiceNumber = table.Column<string>(nullable: true),
                    InvoiceName = table.Column<string>(nullable: true),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    TransportingMiles = table.Column<long>(nullable: false),
                    BOINumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FreightInvoiceHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FreightInvoiceHeaders_FreightInvoices_FreightInvoice_Id",
                        column: x => x.FreightInvoice_Id,
                        principalTable: "FreightInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FreightInvoiceItems_FreightInvoiceHeader_Id",
                table: "FreightInvoiceItems",
                column: "FreightInvoiceHeader_Id");

            migrationBuilder.CreateIndex(
                name: "IX_FreightInvoiceHeaders_FreightInvoice_Id",
                table: "FreightInvoiceHeaders",
                column: "FreightInvoice_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FreightInvoiceItems_FreightInvoiceHeaders_FreightInvoiceHeader_Id",
                table: "FreightInvoiceItems",
                column: "FreightInvoiceHeader_Id",
                principalTable: "FreightInvoiceHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FreightInvoiceItems_FreightInvoiceHeaders_FreightInvoiceHeader_Id",
                table: "FreightInvoiceItems");

            migrationBuilder.DropTable(
                name: "FreightInvoiceHeaders");

            migrationBuilder.DropIndex(
                name: "IX_FreightInvoiceItems_FreightInvoiceHeader_Id",
                table: "FreightInvoiceItems");

            migrationBuilder.DropColumn(
                name: "FreightInvoiceHeader_Id",
                table: "FreightInvoiceItems");

            migrationBuilder.AlterColumn<decimal>(
                name: "GrandTotal",
                table: "FreightInvoices",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 6)");

            migrationBuilder.AddColumn<string>(
                name: "BOINumber",
                table: "FreightInvoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FreightCharges",
                table: "FreightInvoices",
                type: "decimal(18, 6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "InvoiceDate",
                table: "FreightInvoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InvoiceName",
                table: "FreightInvoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "FreightInvoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "FreightInvoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TransportingMiles",
                table: "FreightInvoices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "FreightInvoice_Id",
                table: "FreightInvoiceItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "InvoiceItem_Id",
                table: "FreightInvoiceItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FreightInvoiceItems_InvoiceItem_Id",
                table: "FreightInvoiceItems",
                column: "InvoiceItem_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FreightInvoiceItems_FreightInvoices_InvoiceItem_Id",
                table: "FreightInvoiceItems",
                column: "InvoiceItem_Id",
                principalTable: "FreightInvoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
