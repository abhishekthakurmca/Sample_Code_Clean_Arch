using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class drop_column_cantransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("CanTransfer", "RouteShipments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
