using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Create_AutoSchedule_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutoSchedule",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    CarrierId = table.Column<int>(nullable: false),
                    CollectionPointId = table.Column<int>(nullable: false),
                    DockId = table.Column<int>(nullable: false),
                    SiteId = table.Column<int>(nullable: false),
                    TruckSize = table.Column<int>(nullable: false),
                    TruckSizeName = table.Column<string>(nullable: true),
                    TransitDays = table.Column<int>(nullable: false),
                    Miles = table.Column<decimal>(nullable: false),
                    ClientName = table.Column<string>(nullable: true),
                    CarrierName = table.Column<string>(nullable: true),
                    CollectionPointName = table.Column<string>(nullable: true),
                    FreightType = table.Column<string>(nullable: true),
                    SiteName = table.Column<string>(nullable: true),
                    DockName = table.Column<string>(nullable: true),
                    Rate = table.Column<decimal>(nullable: false),
                    IsPause = table.Column<bool>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoSchedule", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutoSchedule");
        }
    }
}
