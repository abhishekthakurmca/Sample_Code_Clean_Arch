using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class ManageClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "NewQuote",
                table: "Route",
                type: "decimal(18, 3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "ManageClients",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<long>(nullable: false),
                    ClientName = table.Column<string>(nullable: true),
                    COI = table.Column<string>(nullable: true),
                    EquipmentLoc = table.Column<string>(nullable: true),
                    FreightElevatorId = table.Column<int>(nullable: false),
                    TruckCapacityId = table.Column<int>(nullable: false),
                    DroppedTrailerId = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    AutoTender = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManageClients", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManageClients");

            migrationBuilder.AlterColumn<decimal>(
                name: "NewQuote",
                table: "Route",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 3)");
        }
    }
}
