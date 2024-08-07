using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Alter_DockSchedule_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickUpDate",
                table: "DockScheduled");

            migrationBuilder.AlterColumn<string>(
                name: "Capacity",
                table: "DockScheduled",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<int>(
                name: "ReceivingTypeID",
                table: "DockScheduled",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SiteId",
                table: "DockScheduled",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceivingTypeID",
                table: "DockScheduled");

            migrationBuilder.DropColumn(
                name: "SiteId",
                table: "DockScheduled");

            migrationBuilder.AlterColumn<long>(
                name: "Capacity",
                table: "DockScheduled",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PickUpDate",
                table: "DockScheduled",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
