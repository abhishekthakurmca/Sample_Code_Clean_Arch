using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class update_defaultvalue_column_cantransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
               name: "CanTransfer",
               table: "RouteShipments",
               nullable: false,
               defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
