using Microsoft.EntityFrameworkCore.Migrations;

namespace Anubis.Infrastructure.Migrations
{
    public partial class addcolumnstrailernumberandSealnumberinRoutetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SealNumber",
                table: "Route",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrailerNumber",
                table: "Route",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SealNumber",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "TrailerNumber",
                table: "Route");
        }
    }
}
