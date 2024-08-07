using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Update_invoice_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BOLNumber",
                table: "FreightInvoiceItems");

            migrationBuilder.DropColumn(
                name: "InvoiceDate",
                table: "FreightInvoiceItems");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "FreightInvoiceItems");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "FreightInvoiceItems");

            migrationBuilder.DropColumn(
                name: "TransportingMiles",
                table: "FreightInvoiceItems");

            migrationBuilder.AlterColumn<decimal>(
                name: "DefaultPrice",
                table: "WGSAccesrails",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 6)");

            migrationBuilder.AddColumn<string>(
                name: "BOINumber",
                table: "FreightInvoices",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "GrandTotal",
                table: "FreightInvoices",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "InvoiceDate",
                table: "FreightInvoices",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InvoiceName",
                table: "FreightInvoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "FreightInvoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "FreightInvoices",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TransportingMiles",
                table: "FreightInvoices",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BOINumber",
                table: "FreightInvoices");

            migrationBuilder.DropColumn(
                name: "GrandTotal",
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

            migrationBuilder.AlterColumn<decimal>(
                name: "DefaultPrice",
                table: "WGSAccesrails",
                type: "decimal(18, 6)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<string>(
                name: "BOLNumber",
                table: "FreightInvoiceItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InvoiceDate",
                table: "FreightInvoiceItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "FreightInvoiceItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "FreightInvoiceItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TransportingMiles",
                table: "FreightInvoiceItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
