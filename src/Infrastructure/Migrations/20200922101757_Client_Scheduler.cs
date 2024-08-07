using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Client_Scheduler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientSchedule",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    ClientId = table.Column<long>(nullable: false),
                    CollectionPointId = table.Column<long>(nullable: false),
                    ScheduleType = table.Column<string>(nullable: true),
                    WeekOfMonth = table.Column<int>(nullable: false),
                    WeekDay = table.Column<string>(nullable: true),
                    DockTime = table.Column<string>(nullable: true),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSchedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleDates",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<long>(nullable: false),
                    ClientScheduleId = table.Column<long>(nullable: false),
                    CollectionPointId = table.Column<long>(nullable: false),
                    SchedulerDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleDates_ClientSchedule_ClientScheduleId",
                        column: x => x.ClientScheduleId,
                        principalTable: "ClientSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDates_ClientScheduleId",
                table: "ScheduleDates",
                column: "ClientScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleDates");

            migrationBuilder.DropTable(
                name: "ClientSchedule");
        }
    }
}
