using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class Update_Relationship_Routes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountType",
                table: "RouteShipments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "RouteShipments");
        }
    }
}
