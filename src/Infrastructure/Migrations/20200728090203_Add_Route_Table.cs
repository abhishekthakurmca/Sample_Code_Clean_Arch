using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Add_Route_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverName = table.Column<string>(nullable: true),
                    DriverNumber = table.Column<string>(nullable: true),
                    TruckNumber = table.Column<string>(nullable: true),
                    DriverID = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RouteShipments_Route_Id",
                table: "RouteShipments",
                column: "Route_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RouteShipments_Route_Route_Id",
                table: "RouteShipments",
                column: "Route_Id",
                principalTable: "Route",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteShipments_Route_Route_Id",
                table: "RouteShipments");

            migrationBuilder.DropTable(
                name: "Route");

            migrationBuilder.DropIndex(
                name: "IX_RouteShipments_Route_Id",
                table: "RouteShipments");
        }
    }
}
